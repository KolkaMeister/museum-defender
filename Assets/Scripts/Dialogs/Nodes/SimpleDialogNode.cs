using System;

namespace Dialogs.Nodes
{
    [Serializable]
    public class SimpleDialogNode : DialogNode
    {
        public SimpleDialogNode(string text, string name, string tag = "") 
            : base(text, name, tag)
        {
        }
    }
}