using Be.Windows.Forms;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VD.BinarySchema;
using VD.BinarySchema.Parse;

namespace TCPMon
{
    public partial class SchemaTester : Form
    {
        readonly Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        readonly Style KeywordStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        readonly Style ControlStyle = new TextStyle(Brushes.Purple, null, FontStyle.Regular);
        readonly Style ItalicControlStyle = new TextStyle(Brushes.Purple, null, FontStyle.Italic);
        readonly Style BoldStyle = new TextStyle(null, null, FontStyle.Bold);
        readonly Style BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        readonly Style DodgerBlueStyle = new TextStyle(Brushes.DodgerBlue, null, FontStyle.Regular);
        readonly Style MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);


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

        private void fastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(GreenStyle, KeywordStyle, ItalicControlStyle, ControlStyle, BoldStyle, BrownStyle, DodgerBlueStyle, MaroonStyle);
            e.ChangedRange.ClearFoldingMarkers();

            e.ChangedRange.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(BrownStyle, @"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            e.ChangedRange.SetStyle(MaroonStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");

            e.ChangedRange.SetStyle(KeywordStyle, @"\b(struct|enum)\b");
            e.ChangedRange.SetStyle(BoldStyle, @"\b(struct|enum)\s+(?<range>[\w_]+?)\b");
            e.ChangedRange.SetStyle(DodgerBlueStyle, @"\b([\w_]+?)\s+::\s+(?<range>[\w_]+?)\b");
            e.ChangedRange.SetStyle(ItalicControlStyle, @"(?<range>@[\w_]+?)\b");
            e.ChangedRange.SetStyle(ControlStyle, @"\b(if|else|until|included)\b");

            e.ChangedRange.SetFoldingMarkers("{", "}");
        }
    }
}
