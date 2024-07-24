using Be.Windows.Forms;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPMon.Connection;
using VD.BinarySchema;
using VD.BinarySchema.Parse;

namespace TCPMon
{
    public partial class MonitorForm : Form
    {
        private IConnection _connection;

        public MonitorForm()
        {
            InitializeComponent();
        }

        public MonitorForm(IConnection connection) : this()
        {
            _connection = connection;
            _connection.ConnectionClosed += _connection_ConnectionClosed;
            monitorPanel.SetConnection(connection);

            connStrip.Text = $"Connection: {connection.Name} - {connection.Address}";
        }

        private void _connection_ConnectionClosed(IConnection sender)
        {
            Action action = delegate { Close(); };
            Invoke(action);
        }

        private void MonitorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Unregister events
            _connection.ConnectionClosed -= _connection_ConnectionClosed;
            monitorPanel.SetConnection(null);
        }

        public new void CenterToParent()
        {
            base.CenterToParent();
        }

        private void schemaButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Schema files|*.schema",
            };

            fileDialog.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "scripts");
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                string schema = File.ReadAllText(fileDialog.FileName);
                string name = Path.GetFileName(fileDialog.FileName);

                Lexer lexer = new Lexer();
                Parser parser = new Parser();

                var tokens = lexer.Lex(schema, name);
                var defs = parser.Parse(tokens);

                schemaStatus.Text = $"Current Schema: {name}";
                monitorPanel.CurrentSchema = defs;
                monitorPanel.CurrentSchemaName = name;
            }
            catch (LexerException ex)
            {
                MessageBox.Show($"[{ex.Source}:{ex.Line}] {ex.Message}", "Schema error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ParserException ex)
            {
                MessageBox.Show($"[{ex.Source}:{ex.Line}] {ex.Message}", "Schema error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
