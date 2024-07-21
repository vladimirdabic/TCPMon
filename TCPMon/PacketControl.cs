using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPMon
{
    public partial class PacketControl : UserControl
    {
        public byte[] Data { get; private set; }
        public event EventHandler HexClicked;

        public PacketControl()
        {
            InitializeComponent();
        }

        public PacketControl(byte[] bytes, DateTime dateTime) : this()
        {
            dateTimeLabel.Text = dateTime.ToString();
            infoLabel.Text = $"Size: {bytes.Length} bytes";
            Data = bytes;
        }

        private void hexButton_Click(object sender, EventArgs e)
        {
            HexClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
