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
    public partial class ConnectionControl : UserControl
    {
        public IConnection Connection { get; private set; }
        public event EventHandler MonitorClicked;

        public ConnectionControl()
        {
            InitializeComponent();
        }

        public ConnectionControl(IConnection connection) : this()
        {
            Connection = connection;
            conName.Text = $"{Connection.Name} ({Connection.Address})";
        }

        private void monitorButton_Click(object sender, EventArgs e)
        {
            MonitorClicked?.Invoke(this, null);
        }

        private void closeConn_Click(object sender, EventArgs e)
        {
            Connection.Close();
        }
    }
}
