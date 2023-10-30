using Dialogs.Nodes;
using UnityEngine;

namespace Dialogs.Sideline
{
    public class DialogPicker : IDialogPicker
    {
        private readonly IDialogDataService _dialogSvc;

        public DialogPicker(IDialogDataService dialogSvc)
        {
            _dialogSvc = dialogSvc;
        }

        public DialogTree GetRandomDialog(string template)
        {
            var list = _dialogSvc.FindAll(template);
            return list[Random.Range(0, list.Count)];
        }
    }

    public interface IDialogPicker
    {
        public DialogTree GetRandomDialog(string template);
    }
}