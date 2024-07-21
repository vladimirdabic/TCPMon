using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPMon.Connection;

namespace TCPMon
{
    public partial class MainForm : Form
    {
        private ConnectionForm _connForm = new ConnectionForm();
        private List<ConnectionControl> _connectionControls = new List<ConnectionControl>();
        private static RichTextBox _consoleInstance;

        public MainForm()
        {
            InitializeComponent();
            _consoleInstance = consoleBox;
        }

        public static void PrintLine(string message, Color color)
        {
            if (!string.IsNullOrWhiteSpace(_consoleInstance.Text))
            {
                _consoleInstance.AppendText("\r\n" + message, color);
            }
            else
            {
                _consoleInstance.AppendText(message, color);
            }
        }

        public static void PrintLine(string message) { PrintLine(message, Color.Black); }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = _connForm.ShowDialog();
            if (result != DialogResult.OK) return;

            try
            {
                TCPConnection connection = new TCPConnection(_connForm.nameTextBox.Text);
                PrintLine($"[{connection.Name} - {_connForm.ipTextBox.Text}:{_connForm.portTextBox.Text}] Attempting connection...");

                connection.Connect(_connForm.ipTextBox.Text, int.Parse(_connForm.portTextBox.Text));
                connection.ConnectionClosed += Connection_ConnectionClosed;
                connection.PacketReceived += Connection_PacketReceived;

                ConnectionControl control = new ConnectionControl(connection);
                control.MonitorClicked += Control_MonitorClicked;
                control.SendDataClicked += Control_SendDataClicked; ;
                connectionPanel.Controls.Add(control);
                _connectionControls.Add(control);

                activeConns.Text = $"Active Connections: {_connectionControls.Count}";
                PrintLine($"[{connection.Name} - {connection.Address}] Connection established", Color.Green);
            }
            catch(ConnectionException ex)
            {
                PrintLine($"[{_connForm.nameTextBox.Text} - {_connForm.ipTextBox.Text}:{_connForm.portTextBox.Text}] Connection failed: {ex.Message}", Color.Orange);
            }
            catch(FormatException)
            {
                PrintLine($"[{_connForm.nameTextBox.Text} - {_connForm.ipTextBox.Text}:{_connForm.portTextBox.Text}] Connection failed: port must be a number", Color.Orange);
            }
        }

        private void Connection_PacketReceived(IConnection sender, IPacket packet)
        {
            Action action = () => PrintLine($"[{sender.Name} - {sender.Address}] Received {packet.Data.Length} bytes", Color.Gray);
            Invoke(action);
        }

        private void Control_SendDataClicked(object sender, EventArgs e)
        {
            IConnection connection = ((ConnectionControl)sender).Connection;
            SendForm sendForm = new SendForm(connection);
            sendForm.Show();
        }

        private void Control_MonitorClicked(object sender, EventArgs e)
        {
            IConnection connection = ((ConnectionControl)sender).Connection;
            MonitorForm monitorForm = new MonitorForm(connection);
            monitorForm.Show();
        }

        private void Connection_ConnectionClosed(IConnection sender)
        {
            Action action = delegate
            {
                foreach (ConnectionControl control in _connectionControls)
                {
                    if (control.Connection == sender)
                    {
                        _connectionControls.Remove(control);
                        connectionPanel.Controls.Remove(control);
                        control.Dispose();
                        break;
                    }
                }

                activeConns.Text = $"Active Connections: {_connectionControls.Count}";
                PrintLine($"[{sender.Name} - {sender.Address}] Connection closed", Color.Red);

                sender.PacketReceived -= Connection_PacketReceived;
            };

            Invoke(action);
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ConnectionControl control in _connectionControls)
                control.Connection.Close();
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}

