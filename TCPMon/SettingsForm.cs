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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            blzcPath.Text = Properties.Settings.Default.BlazePath;
            useDebug.Checked = Properties.Settings.Default.BlazeDebug;
            simpleModuleName.Checked = Properties.Settings.Default.BlazeSimplify;
            printByteData.Checked = Properties.Settings.Default.ConsoleByteInfo;
        }

        private void useDebug_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.BlazeDebug = useDebug.Checked;
        }

        private void simpleModuleName_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.BlazeSimplify = simpleModuleName.Checked;
        }

        private void setBlazePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Executable Files|*.exe"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            blzcPath.Text = ofd.FileName;
            Properties.Settings.Default.BlazePath = ofd.FileName;
        }

        private void printByteData_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ConsoleByteInfo = printByteData.Checked;
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
