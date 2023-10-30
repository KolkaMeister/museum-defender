using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Dialogs.Nodes
{
    public static class DialogBuilder
    {
        private const string TEXT_ELEM = "Text";

        private const string AUTHOR_ATTR = "author";
        private const string LAY_ATTR = "lay";
        private const string LINE_ATTR = "line";
        private const string NAME_ATTR = "name";
        private const string REF_ATTR = "ref";
        private const string TAG_ATTR = "tag";

        private const string EMPTY = "\0";

        private static DialogNode _startNode;
        private static Stack<DialogNode> _nodeStack;
        private static Stack<string> _pathStack;

        public static DialogTree Build(string path)
        {
            // For storing parents. New node is attached to leafs of parent we are inside. 
            _nodeStack = new Stack<DialogNode>();
            
            // For access to current file path. We need know path
            // when we have built part dialog and we return to parent dialog file. 
            _pathStack = new Stack<string>();
            
            DialogTree tree = null;
            XmlNode root = GetDialogRoot(path);
            if (root != null)
            {
                _pathStack.Push(path);
                tree = new DialogTree();
                string name = GetAttribute(root, NAME_ATTR);
                CheckAttribute(name, root.Name);

                tree.Name = name;
                FillLinearDialog(root);

                tree.Root = _startNode;
                _pathStack.Pop();
            }

            return tree;
        }

        private static void AddNode(DialogNode node, DialogData data)
        {
            if (_nodeStack.TryPeek(out DialogNode parent))
                // Answer branches may merge into one phrase.
                // So we find leaves depending on what line they lay on and attach they to node 
                FillLeaves(GetLeaves(parent, data.Lay), node);
            else
                FillRoot(node);
        }

        private static void BuildPartDialog(string path)
        {
            XmlNode root = GetDialogRoot(path);
            if (root != null)
            {
                FillLinearDialog(root);
            }
        }

        private static void CheckAttribute(string attribute, string debugElement)
        {
            if (attribute == EMPTY)
                throw new XmlException($"The {debugElement} attributes is not defined");
        }

        private static void FillAnswer(XmlNode child)
        {
            string refAttr = GetAttribute(child, REF_ATTR);
            if (refAttr != EMPTY)
            {
                _pathStack.Push(Path.GetDirectoryName(_pathStack.Peek()) + "\\" + refAttr);
                BuildPartDialog(_pathStack.Peek());
                _pathStack.Pop();
                return;
            }

            FillLinearDialog(child);
        }

        private static void FillBranchDialog(XmlNode root)
        {
            var parent = (BranchDialogNode)_nodeStack.Peek();
            foreach (XmlNode child in root.ChildNodes)
            {
                if (child.Name == TEXT_ELEM) continue;

                DialogData data = GetNodeData(child);
                var answer = new AnswerNode(data.Text, data.Line, data.Tag);
                parent.Answers.Add(answer);

                _nodeStack.Push(answer);
                FillAnswer(child);
                _nodeStack.Pop();
            }
        }

        private static void FillLeaves(List<DialogNode> leaves, DialogNode node)
        {
            foreach (DialogNode leaf in leaves)
            {
                leaf.Child = node;
                node.Parent = leaf;
            }
        }

        private static void FillLinearDialog(XmlNode root)
        {
            foreach (XmlNode child in root.ChildNodes)
            {
                if (child.Name == TEXT_ELEM) continue;
                CheckAttribute(GetAttribute(child, AUTHOR_ATTR), child.Name);
                
                DialogData data = GetNodeData(child);
                DialogNode node = GetDialogNode(child, data);

                AddNode(node, data);
            }
        }

        private static void FillRoot(DialogNode node)
        {
            _startNode = node;
            _nodeStack.Push(_startNode);
        }

        private static string GetAttribute(XmlNode node, string name, string empty = EMPTY) => node?.Attributes?[name]?.Value ?? empty;

        private static DialogNode GetDialogNode(XmlNode xNode, DialogData data)
        {
            if (xNode.ChildNodes.Count > 1)
            {
                var branchNode = new BranchDialogNode(data.Text, data.Author, data.Tag);
                _nodeStack.Push(branchNode);
                FillBranchDialog(xNode);
                return _nodeStack.Pop();
            }

            return new SimpleDialogNode(data.Text, data.Author, data.Tag);
        }

        private static XmlNode GetDialogRoot(string path)
        {
            var doc = new XmlDocument();
            doc.Load(path);
            
            // We return root first child because root itself uses for setting namespace.
            return doc.DocumentElement?.FirstChild;
        }

        private static List<DialogNode> GetLeaves(DialogNode parent, string lay)
        {
            if (lay == "") return parent.GetLeaves();

            string[] lays = SplitLayValues(lay);
            
            // Set is used because answers may have the same leaves for merging.
            var leaves = new HashSet<DialogNode>();
            foreach (DialogNode answer in parent.GetAllChildrenAndSelf().Where(x => MatchLines(x, lays)))
                leaves.UnionWith(answer.GetLeaves());

            return leaves.ToList();
        }

        private static DialogData GetNodeData(XmlNode node)
        {
            return new DialogData
            {
                Text = node.FirstChild.InnerText.Trim(),
                Author = GetAttribute(node, AUTHOR_ATTR, ""),
                Line = GetAttribute(node, LINE_ATTR, ""),
                Lay = GetAttribute(node, LAY_ATTR, ""),
                Tag = GetAttribute(node, TAG_ATTR, "")
            };
        }

        private static bool MatchLines(DialogNode node, string[] lines) =>
            node is AnswerNode ans && lines.Contains(ans.Line);

        private static string[] SplitLayValues(string line) =>
            line.Replace(" ", "").Split(",");
    }

    public struct DialogData
    {
        public string Text;
        public string Author;
        public string Line;
        public string Lay;
        public string Tag;
    }
}