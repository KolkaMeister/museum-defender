using System;
using System.Collections.Generic;
using System.Linq;

namespace Dialogs.Nodes
{
    [Serializable]
    public class BranchDialogNode : DialogNode
    {
        public List<DialogNode> Answers = new List<DialogNode>();

        public BranchDialogNode(string text, string name, string tag = "")
            : base(text, name, tag, PhraseType.Branch)
        {
        }

        public override List<DialogNode> GetAllChildrenAndSelf()
        {
            // Set is used because answers may have the same leaves for merging.
            var set = new HashSet<DialogNode>() { this };
            foreach (DialogNode answer in Answers)
                set.UnionWith(answer.GetAllChildrenAndSelf());
            return set.ToList();
        }

        public override List<DialogNode> GetLeaves()
        {
            // Set is used because answers may have the same leaves for merging.
            var leaves = new HashSet<DialogNode>();
            foreach (DialogNode answer in Answers)
                leaves.UnionWith(answer.GetLeaves());
            return leaves.ToList();
        }

        public override DialogNode Find(string tag)
        {
            return Tag == tag 
                ? this 
                : Answers.Select(answer => answer.Find(tag))
                    .FirstOrDefault(result => result != null);
        }
    }
}