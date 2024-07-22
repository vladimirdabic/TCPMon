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
using VD.BinarySchema;

namespace TCPMon
{
    public partial class SchemaTester : Form
    {
        public SchemaTester()
        {
            InitializeComponent();

            packetHexBox.ByteProvider = new DynamicByteProvider(new byte[0]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = SchemaDecoder.Decode(((DynamicByteProvider)packetHexBox.ByteProvider).Bytes.ToArray(), fastColoredTextBox1.Text);
                PrintDict(obj);

            }
            catch(LexerException ex)
            {
                Console.WriteLine($"[{ex.Source}:{ex.Line}] {ex.Message}");
            }
            catch(ParserException ex)
            {
                Console.WriteLine($"[{ex.Source}:{ex.Line}] {ex.Message}");
            }
            catch(DecoderException ex)
            {
                Console.WriteLine($"[{ex.Source}:{ex.Line}] {ex.Message}");
            }
        }

        private void PrintDict(Dictionary<string, object> dict, int level = 0)
        {
            foreach (var entry in dict)
            {
                if (entry.Value is Dictionary<string, object> subDict)
                    PrintDict(subDict, level + 1);
                else
                    Console.WriteLine(new string(' ', level * 4) + entry.Key + ": " + entry.Value);
            }
        }
    }
}
