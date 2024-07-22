using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPMon.Connection;

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
    }
}
