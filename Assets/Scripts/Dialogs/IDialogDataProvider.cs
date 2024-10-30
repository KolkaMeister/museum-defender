using System.Collections.Generic;
using Dialogs.Nodes;

namespace Dialogs
{
    public interface IDialogDataProvider
    {
        public DialogTree Find(string name);
        public List<DialogTree> FindAll(string template);
    }
}