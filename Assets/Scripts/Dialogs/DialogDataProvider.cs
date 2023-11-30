using System.Collections.Generic;
using System.IO;
using Dialogs.Nodes;
using UnityEngine;
using Zenject;

namespace Dialogs
{
    public class DialogDataProvider : IInitializable, IDialogDataProvider
    {
        private readonly DialogSo _dialogSo;
        private readonly List<DialogTree> _dialogs = new List<DialogTree>();

        public DialogDataProvider(DialogSo dialogSo)
        {
            _dialogSo = dialogSo;
        }

        public void Initialize()
        {
            foreach (string file in _dialogSo.FileNames)
            {
                DialogTree tree = DialogBuilder.Build(Path.Combine(Application.streamingAssetsPath, file));
                _dialogs.Add(tree);
            }
        }

        public DialogTree Find(string name) => _dialogs.Find(x => x.Name == name);
        public List<DialogTree> FindAll(string template) => _dialogs.FindAll(x => x.Name.StartsWith(template));
    }
}