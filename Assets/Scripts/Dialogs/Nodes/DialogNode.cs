using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogs.Nodes
{
    [Serializable]
    public class DialogNode
    {
        public DialogNode Child;
        public DialogNode Parent;

        [field: SerializeField] public string Name { get; private set; }
        [field: TextArea(minLines: 3, maxLines: 10)][field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public string Tag { get; private set; }
        [field: SerializeField] public PhraseType Phrase { get; private set; }

        public Action OnPhraseStarted;

        protected DialogNode(string text, string name, string tag = "", PhraseType phrase = PhraseType.Speech)
        {
            Text = text;
            Name = name;
            Tag = tag;
            Phrase = phrase;
        }

        public virtual List<DialogNode> GetAllChildrenAndSelf()
        {
            var nodes = new List<DialogNode> { this };
            if(Child != null)
                nodes.AddRange(Child.GetAllChildrenAndSelf());

            return nodes;
        }

        public virtual List<DialogNode> GetLeaves()
        {
            return Child == null ? new List<DialogNode> { this } : Child.GetLeaves();
        }

        public virtual DialogNode Find(string tag)
        {
            return Tag == tag ? this : Child?.Find(tag);
        }
    }
}