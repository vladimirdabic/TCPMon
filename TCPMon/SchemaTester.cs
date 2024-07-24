using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VD.BinarySchema;
using VD.BinarySchema.Parse;

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
                byte[] data = ((DynamicByteProvider)packetHexBox.ByteProvider).Bytes.ToArray();

                Lexer lexer = new Lexer();
                Parser parser = new Parser();
                TreeSchema treeSchema = new TreeSchema(treeView1);
                SchemaDecoder decoder = new SchemaDecoder();

                var tokens = lexer.Lex(fastColoredTextBox1.Text, "<internal>");
                var defs = parser.Parse(tokens);

                BinaryReader reader = new BinaryReader(new MemoryStream(data));
                SchemaObject obj = decoder.Decode(reader, defs, "<internal>");

                treeSchema.LoadSchema(obj);
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
    }
}
