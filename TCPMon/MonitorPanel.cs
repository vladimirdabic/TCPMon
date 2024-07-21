using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPMon.Connection;

namespace TCPMon
{
    public partial class MonitorPanel : UserControl
    {
        public IConnection Connection { get; set; }

        public MonitorPanel()
        {
            InitializeComponent();
            Connection = null;  

            packetListPanel.VerticalScroll.Visible = true;
        }

        public void SetConnection(IConnection connection)
        {
            // Unregister events for previous connection
            if(Connection != null)
            {
                Connection.PacketReceived -= Connection_PacketReceived;
            }

            if (connection == null) return;

            Connection = connection;
            connection.PacketReceived += Connection_PacketReceived;

            packetListPanel.Controls.Clear();
            packetHexBox.ByteProvider = null;

            foreach (IPacket packet in Connection.Packets)
            {
                PacketControl control = new PacketControl(packet.Data, packet.ReceivedAt);
                packetListPanel.Controls.Add(control);
                control.HexClicked += Control_HexClicked;
            }
        }

        private void Control_HexClicked(object sender, EventArgs e)
        {
            packetHexBox.ByteProvider = new DynamicByteProvider(((PacketControl)sender).Data);
        }

        private void Connection_PacketReceived(IConnection sender, IPacket packet)
        {
            Action action = delegate
            {
                PacketControl control = new PacketControl(packet.Data, packet.ReceivedAt);
                packetListPanel.Controls.Add(control);
                control.HexClicked += Control_HexClicked;
            };
            Invoke(action);
        }

        private void clearPackets_Click(object sender, EventArgs e)
        {
            Connection.ClearPackets();
            packetListPanel.Controls.Clear();
            packetHexBox.ByteProvider = null;
        }
    }
}
