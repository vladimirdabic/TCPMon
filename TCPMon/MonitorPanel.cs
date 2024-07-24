using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
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
    public partial class MonitorPanel : UserControl
    {
        public IConnection Connection { get; private set; }
        public Statement CurrentSchema { get; set; }
        public string CurrentSchemaName { get; set; }

        private SchemaDecoder _decoder;
        private TreeSchema _treeSchema;

        public MonitorPanel()
        {
            InitializeComponent();

            Connection = null;
            _decoder = new SchemaDecoder();
            _treeSchema = new TreeSchema(schemaTree);
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
            byte[] data = ((PacketControl)sender).Data;
            packetHexBox.ByteProvider = new DynamicByteProvider(data);

            if (CurrentSchema is null) return;

            try
            {
                BinaryReader reader = new BinaryReader(new MemoryStream(data));
                SchemaObject obj = _decoder.Decode(reader, CurrentSchema, CurrentSchemaName);
                _treeSchema.LoadSchema(obj);
            }
            catch (DecoderException ex)
            {
                MessageBox.Show($"[{ex.Source}:{ex.Line}] {ex.Message}", "Schema error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Connection_PacketReceived(IConnection sender, IPacket packet)
        {
            Action action = delegate
            {
                PacketControl control = new PacketControl(packet.Data, packet.ReceivedAt);
                packetListPanel.Controls.Add(control);
                control.HexClicked += Control_HexClicked;
            };
            
            this?.Invoke(action);
        }

        private void clearPackets_Click(object sender, EventArgs e)
        {
            Connection.ClearPackets();
            packetListPanel.Controls.Clear();
            packetHexBox.ByteProvider = null;
        }
    }
}
