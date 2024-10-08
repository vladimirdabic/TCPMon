﻿using Be.Windows.Forms;
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
        private readonly NewForm _newForm = new NewForm();
        private readonly SettingsForm _settingsForm = new SettingsForm();
        private readonly AboutForm _aboutForm = new AboutForm();
        private readonly List<ConnectionControl> _connectionControls = new List<ConnectionControl>();
        private static RichTextBox _consoleInstance;

        public MainForm()
        {
            InitializeComponent();
            _consoleInstance = consoleBox;

            // new SchemaTester().Show();
        }

        public static void PrintLine(string message, Color color)
        {
            Action action = delegate
            {
                if (!string.IsNullOrWhiteSpace(_consoleInstance.Text))
                {
                    _consoleInstance.AppendText("\r\n" + message, color);
                }
                else
                {
                    _consoleInstance.AppendText(message, color);
                }
            };

            _consoleInstance.Invoke(action);
        }

        public static void PrintLine(string message) { PrintLine(message, Color.Black); }

        private void Connection_PacketReceived(IConnection sender, IPacket packet)
        {
            if (!Properties.Settings.Default.ConsoleByteInfo) return;
            
            Action action = () => PrintLine($"[{sender.Name} - {sender.Address}] Received {packet.Data.Length} bytes", Color.Gray);
            Invoke(action);
        }

        private void Control_SendDataClicked(object sender, EventArgs e)
        {
            IConnection connection = ((ConnectionControl)sender).Connection;
            SendForm sendForm = new SendForm(connection);
            sendForm.Show(this);
            sendForm.CenterToParent();
        }

        private void Control_MonitorClicked(object sender, EventArgs e)
        {
            IConnection connection = ((ConnectionControl)sender).Connection;
            MonitorForm monitorForm = new MonitorForm(connection);
            monitorForm.Show(this);
            monitorForm.CenterToParent();
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

        private void newClientStrip_Click(object sender, EventArgs e)
        {
            _newForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = _newForm.ShowDialog();
            if (result != DialogResult.OK) return;

            try
            {
                IConnection connection;
                PrintLine($"[{_newForm.ConnectionName} - {_newForm.Parameters.IPAddress}:{_newForm.Parameters.Port}] Attempting connection...");

                switch (_newForm.ConnectionType)
                {
                    case "Basic":
                        TCPConnection tcpConnection = new TCPConnection(_newForm.ConnectionName);
                        connection = tcpConnection;
                        tcpConnection.Connect(_newForm.Parameters.IPAddress, _newForm.Parameters.Port);
                        break;

                    case "Scripted":
                        TCPScriptedConnection scriptedConn = new TCPScriptedConnection(_newForm.ConnectionName, ((ScriptedConnectionParameters)_newForm.Parameters).FilePath);
                        connection = scriptedConn;
                        scriptedConn.Connect(_newForm.Parameters.IPAddress, _newForm.Parameters.Port);
                        break;

                    default:
                        throw new Exception("Unknown connection type");
                }

                connection.ConnectionClosed += Connection_ConnectionClosed;
                connection.PacketReceived += Connection_PacketReceived;

                ConnectionControl control = new ConnectionControl(connection);
                control.MonitorClicked += Control_MonitorClicked;
                control.SendDataClicked += Control_SendDataClicked;
                connectionPanel.Controls.Add(control);
                _connectionControls.Add(control);

                activeConns.Text = $"Active Connections: {_connectionControls.Count}";
                PrintLine($"[{connection.Name} - {connection.Address}] Connection established", Color.Green);
            }
            catch (ConnectionException ex)
            {
                PrintLine($"[{_newForm.ConnectionName} - {_newForm.Parameters.IPAddress}:{_newForm.Parameters.Port}] Connection failed: {ex.Message}", Color.Orange);
            }
            catch (FormatException)
            {
                PrintLine($"[{_newForm.ConnectionName} - {_newForm.Parameters.IPAddress}:{_newForm.Parameters.Port}] Connection failed: Port must be a number", Color.Orange);
            }
        }

        private void closeConnsStrip_Click(object sender, EventArgs e)
        {
            foreach (ConnectionControl control in _connectionControls)
                control.Connection.Close();
        }

        private void editorStrip_Click(object sender, EventArgs e)
        {
            ScriptEditor editorForm = new ScriptEditor();
            editorForm.Show(this);
            editorForm.CenterToParent();
        }

        private void settingsStrip_Click(object sender, EventArgs e)
        {
            _settingsForm.StartPosition = FormStartPosition.CenterParent;
            _settingsForm.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _aboutForm.StartPosition = FormStartPosition.CenterParent;
            _aboutForm.ShowDialog();
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

