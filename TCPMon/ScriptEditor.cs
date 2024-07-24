using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TCPMon
{
    public partial class ScriptEditor : Form
    {
        readonly Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        readonly Style KeywordStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        readonly Style ControlStyle = new TextStyle(Brushes.Purple, null, FontStyle.Regular);
        readonly Style ItalicControlStyle = new TextStyle(Brushes.Purple, null, FontStyle.Italic);
        readonly Style BoldStyle = new TextStyle(null, null, FontStyle.Bold);
        readonly Style BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        readonly Style DodgerBlueStyle = new TextStyle(Brushes.DodgerBlue, null, FontStyle.Regular);
        readonly Style MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);

        private string _currentFileExt;
        private string _currentFilePath;
        private TreeNode _clickedNode;

        public ScriptEditor()
        {
            InitializeComponent();

            KeyPreview = true;


            if (!Directory.Exists("scripts"))
                Directory.CreateDirectory("scripts");

            ListDirectory(treeView1, "scripts");
        }

        private void fastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(GreenStyle, KeywordStyle, ItalicControlStyle, ControlStyle, BoldStyle, BrownStyle, DodgerBlueStyle, MaroonStyle);
            e.ChangedRange.ClearFoldingMarkers();

            e.ChangedRange.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(BrownStyle, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(MaroonStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");

            if(_currentFileExt == ".blz")
            {
                e.ChangedRange.SetStyle(KeywordStyle, @"\b(class|func|event|static|extern|var|new|private|public)\b");
                e.ChangedRange.SetStyle(ControlStyle, @"\b(return|for|while|if|break|continue|throw|try|catch)\b");
                e.ChangedRange.SetStyle(BoldStyle, @"\b(class)\s+(?<range>[\w_]+?)\b");
                e.ChangedRange.SetStyle(ItalicControlStyle, @"\b(?<range>iter)\s+([\w_]+?)\b");
            }
            else if(_currentFileExt == ".schema")
            {
                e.ChangedRange.SetStyle(KeywordStyle, @"\b(struct|enum)\b");
                e.ChangedRange.SetStyle(BoldStyle, @"\b(struct|enum)\s+(?<range>[\w_]+?)\b");
                e.ChangedRange.SetStyle(DodgerBlueStyle, @"\b([\w_]+?)\s+::\s+(?<range>[\w_]+?)\b");
                e.ChangedRange.SetStyle(ItalicControlStyle, @"(?<range>@[\w_]+?)\b");
                e.ChangedRange.SetStyle(ControlStyle, @"\b(if|until|included)\b");
            }

            e.ChangedRange.SetFoldingMarkers("{", "}");
        }

        private void blazeModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Text = @"extern var console;
extern var connection;


func main() {
    console.print(""Hello World"");
}


event connection.received(data) {

}

event connection.closed() {

}

";

            _currentFileExt = ".blz";
            _currentFilePath = null;
            fastColoredTextBox1.OnTextChanged(0, fastColoredTextBox1.LinesCount - 1);
        }

        private void binarySchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Text = @"@entry
struct main {

}

";

            _currentFileExt = ".schema";
            _currentFilePath = null;
            fastColoredTextBox1.OnTextChanged(0, fastColoredTextBox1.LinesCount - 1);
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            var savedExpansionState = treeView.Nodes.GetExpansionState();

            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);

            foreach (TreeNode node in CreateDirectoryNode(rootDirectoryInfo).Nodes)
                treeView.Nodes.Add(node);

            treeView.Nodes.SetExpansionState(savedExpansionState);
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name)
            {
                SelectedImageKey = "folder",
                ImageKey = "folder"
            };

            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));

            List<TreeNode> moduleFiles = new List<TreeNode>();
            List<TreeNode> files = new List<TreeNode>();

            foreach (var file in directoryInfo.GetFiles())
            {
                TreeNode node = new TreeNode(file.Name);

                switch(file.Extension)
                {
                    case ".blz":
                        node.ImageKey = "blaze_file";
                        files.Add(node);
                        break;

                    case ".blzm":
                        node.ImageKey = "blaze_module";
                        moduleFiles.Add(node);
                        break;

                    case ".schema":
                        node.ImageKey = "schema_file";
                        files.Add(node);
                        break;

                    default:
                        continue;
                }

                node.SelectedImageKey = node.ImageKey;
            }

            directoryNode.Nodes.AddRange(moduleFiles.ToArray());
            directoryNode.Nodes.AddRange(files.ToArray());

            return directoryNode;
        }

        private void ScriptEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                Save();
                e.SuppressKeyPress = true;
            }
        }

        private void Save()
        {
            if (GetSavePath() == null)
                return;


            bool reload = !File.Exists(_currentFilePath);
            File.WriteAllText(_currentFilePath, fastColoredTextBox1.Text);
            
            if(reload) ListDirectory(treeView1, "scripts");
        }

        private string GetSavePath()
        {
            if (_currentFileExt == null) return null;
            if (_currentFilePath != null) return _currentFilePath;

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = $"|*{_currentFileExt}",
            };

            saveFileDialog.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "scripts");
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                _currentFilePath = saveFileDialog.FileName;

            return _currentFilePath;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = Path.Combine("scripts", treeView1.SelectedNode.FullPath);
            string ext = Path.GetExtension(path);

            if (Directory.Exists(path)) return;
            if (ext == ".blzm") return;

            _currentFilePath = path;
            _currentFileExt = ext;
            fastColoredTextBox1.Text = File.ReadAllText(_currentFilePath);
        }

        private void compileStrip_Click(object sender, EventArgs e)
        {
            Save();

            if(!File.Exists(Properties.Settings.Default.BlazePath))
            {
                textBox1.Text = "Compiler path not defined";
                return;
            }

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = Properties.Settings.Default.BlazePath;
            p.StartInfo.WorkingDirectory = Path.GetDirectoryName(_currentFilePath);

            string buildParameters = $"-s \"{Path.GetFileName(_currentFilePath)}\" ";
            if (Properties.Settings.Default.BlazeDebug) buildParameters += "-d ";
            if (Properties.Settings.Default.BlazeSimplify) buildParameters += $"-m \"{Path.GetFileNameWithoutExtension(_currentFilePath)}\" ";

            p.StartInfo.Arguments = buildParameters;
            p.Start();

            textBox1.Text = $"{Properties.Settings.Default.BlazePath} {buildParameters}\r\n\r\n";

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            textBox1.Text += output;
            ListDirectory(treeView1, "scripts");
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _clickedNode = e.Node;
                contextMenuStrip1.Show(treeView1, e.Location);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_clickedNode.ImageKey == "folder")
                Directory.Delete(Path.Combine("scripts", _clickedNode.FullPath), true);
            else
                File.Delete(Path.Combine("scripts", _clickedNode.FullPath));

            ListDirectory(treeView1, "scripts");
        }

        public new void CenterToParent()
        {
            base.CenterToParent();
        }

        private void saveStrip_Click(object sender, EventArgs e)
        {
            Save();
        }
    }

    public static class TreeViewExtensions
    {
        public static List<string> GetExpansionState(this TreeNodeCollection nodes)
        {
            return nodes.Descendants()
                        .Where(n => n.IsExpanded)
                        .Select(n => n.FullPath)
                        .ToList();
        }

        public static void SetExpansionState(this TreeNodeCollection nodes, List<string> savedExpansionState)
        {
            foreach (var node in nodes.Descendants()
                                      .Where(n => savedExpansionState.Contains(n.FullPath)))
            {
                node.Expand();
            }
        }

        public static IEnumerable<TreeNode> Descendants(this TreeNodeCollection c)
        {
            foreach (var node in c.OfType<TreeNode>())
            {
                yield return node;

                foreach (var child in node.Nodes.Descendants())
                {
                    yield return child;
                }
            }
        }
    }
}
