using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using VD.BinarySchema;
using VD.BinarySchema.Parse;
using VD.Blaze.Interpreter.Types;


namespace TCPMon
{
    public class TreeSchema
    {
        public bool ShowTypes { get; set; }
        public TreeView Tree { get => _tree; set => _tree = value; }

        private TreeView _tree;

        public TreeSchema(TreeView tree)
        {
            _tree = tree;
        }

        public void LoadSchema(SchemaObject obj)
        {
            var savedExpansionState = _tree.Nodes.GetExpansionState();
            _tree.Nodes.Clear();

            foreach (var entry in obj)
                _tree.Nodes.Add(CreateTreeNode(entry.Key, entry.Value));

            _tree.Nodes.SetExpansionState(savedExpansionState);
        }

        private TreeNode CreateTreeNode(string name, DecodedValue value)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(name);
            if (ShowTypes) sb.Append($" ({value.Type.Name})");

            if (value.Value is DecodedValue[] array)
            {
                TreeNode arrayNode = new TreeNode(sb.ToString())
                {
                    ImageKey = "array",
                    SelectedImageKey = "array"
                };

                for(int i = 0; i < array.Length; ++i)
                    arrayNode.Nodes.Add(CreateTreeNode(i.ToString(), array[i]));

                return arrayNode;
            }

            if(value.Value is SchemaObject obj)
            {
                TreeNode objectNode = new TreeNode(sb.ToString())
                {
                    ImageKey = "struct",
                    SelectedImageKey = "struct"
                };

                foreach(var entry in obj)
                    objectNode.Nodes.Add(CreateTreeNode(entry.Key, entry.Value));

                return objectNode;
            }

            sb.Append($" :: {value.Value}");

            string key = value.Type is IntegerType ? "int" : value.Type.Name;

            TreeNode node = new TreeNode(sb.ToString())
            {
                ImageKey = key,
                SelectedImageKey = key
            };

            return node;
        }
    }
}
