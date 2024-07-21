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

        public MainForm()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = _connForm.ShowDialog();
            if (result != DialogResult.OK) return;

            try
            {
                TCPConnection connection = new TCPConnection(_connForm.nameTextBox.Text);
                connection.Connect(_connForm.ipTextBox.Text, int.Parse(_connForm.portTextBox.Text));
                connection.ConnectionClosed += Connection_ConnectionClosed;

                ConnectionControl control = new ConnectionControl(connection);
                control.MonitorClicked += Control_MonitorClicked;
                connectionPanel.Controls.Add(control);
                _connectionControls.Add(control);

                activeConns.Text = $"Active Connections: {_connectionControls.Count}";
            }
            catch(ConnectionException ex)
            {
                MessageBox.Show(ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Control_MonitorClicked(object sender, EventArgs e)
        {
            IConnection connection = ((ConnectionControl)sender).Connection;

            monitorPanel.Visible = true;
            monitorPanel.SetConnection(connection);
        }

        private void Connection_ConnectionClosed(IConnection sender)
        {
            Action a = delegate
            {
                if(monitorPanel.Connection == sender)
                    monitorPanel.Visible = false;

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
            };

            Invoke(a);
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ConnectionControl control in _connectionControls)
                control.Connection.Close();
        }
    }
}
