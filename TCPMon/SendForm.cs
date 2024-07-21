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
    public partial class SendForm : Form
    {
        private IConnection _connection;
        private DynamicByteProvider _provider;

        public SendForm()
        {
            InitializeComponent();

            _provider = new DynamicByteProvider(new byte[] { });
            packetHexBox.ByteProvider = _provider;
        }

        public SendForm(IConnection connection) : this()
        {
            _connection = connection;
            _connection.ConnectionClosed += _connection_ConnectionClosed;

            connStrip.Text = $"Connection: {connection.Name} - {connection.Address}";
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            _connection.Send(_provider.Bytes.ToArray());
            MainForm.PrintLine($"[{_connection.Name} - {_connection.Address}] Sent {_provider.Length} bytes");
        }

        private void _connection_ConnectionClosed(IConnection sender)
        {
            Action action = delegate { Close(); };
            Invoke(action);
        }

        private void SendForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Unregister events
            _connection.ConnectionClosed -= _connection_ConnectionClosed;
        }
    }
}
