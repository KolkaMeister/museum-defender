using System.Collections.Generic;
using System.Linq;

namespace Dialogs.Nodes
{
    public class DialogTree
    {
        public DialogNode Root;
        public string Name;
        
        private HashSet<string> _speakers = new HashSet<string>();
        private List<DialogNode> _cache;

        public List<string> Speakers => _speakers.ToList();

        public bool AddSpeaker(string name)
        {
            return _speakers.Add(name);
        }
        
        public List<DialogNode> GetAllNodes(bool force = false)
        {
            if (_cache == null || force)
            {
                _cache = Root?.GetAllChildrenAndSelf();
            }

            return _cache;
        }

        public List<DialogNode> GetLeafs()
        {
            return GetAllNodes()?
                .Where(x => x is not BranchDialogNode && x.Child == null)
                .ToList();
        }

        public DialogNode Find(string tag)
        {
            return Root?.Find(tag);
        }
    }
}