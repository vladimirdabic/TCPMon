using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace TCPMon
{
    public partial class NewForm : Form
    {
        public ConnectionParameters Parameters { get; private set; }
        public string ConnectionName { get; private set; }
        public string ConnectionType { get; private set; }


        public NewForm()
        {
            InitializeComponent();
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            ConnectionName = nameBox.Text.Length < 1 ? ConnectionType : nameBox.Text;
        }

        private void typeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeList.SelectedItems.Count != 1) return;
            ListViewItem item = typeList.SelectedItems[0];
            ConnectionType = item.Text;


            switch (item.Text)
            {
                case "Basic":
                    Parameters = new ConnectionParameters();
                    descBox.Text = "A basic client connection";
                    break;

                case "Scripted":
                    Parameters = new ScriptedConnectionParameters();
                    descBox.Text = "Send and receive data using blaze scripts";
                    break;
            }

            parameterGrid.SelectedObject = Parameters;
            createBtn.Enabled = true;
        }
    }

    public class ConnectionParameters
    {
        private string _ipAddress = "localhost";
        private int _port = 80;

        [Category("Server Address")]
        [DisplayName("IP Address")]
        [Description("The internet protocol address of the target server")]
        [DefaultValue("localhost")]
        public string IPAddress { get => _ipAddress; set => _ipAddress = value; }

        [Category("Server Address")]
        [Description("The target port")]
        [DefaultValue(80)]
        public int Port { get => _port; set => _port = value; }
    }

    public class ScriptedConnectionParameters : ConnectionParameters
    {
        private string _scriptPath;

        [Category("Script")]
        [DisplayName("File Path")]
        [Description("Path to the script file")]
        [Editor(typeof(BlazeFileNameEditor), typeof(UITypeEditor))]
        public string FilePath { get => _scriptPath; set => _scriptPath = value; }
    }

    public class BlazeFileNameEditor : FileNameEditor
    {
        protected override void InitializeDialog(OpenFileDialog openFileDialog)
        {
            base.InitializeDialog(openFileDialog);
            openFileDialog.Filter = "Blaze Module files (*.blzm)|*.blzm";
        }
    }
}
