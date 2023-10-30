using System.Collections.Generic;
using System.Linq;
using Dialogs.Nodes;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Dialogs
{
    public class DialogDataService : IInitializable, IDialogDataService
    {
        private readonly DialogSo _dialogSo;
        private readonly List<DialogTree> _dialogs = new List<DialogTree>();

        public DialogDataService(DialogSo dialogSo)
        {
            _dialogSo = dialogSo;
        }

        public void Initialize()
        {
            foreach (TextAsset file in _dialogSo.XmlDialogs)
            {
                DialogTree tree = DialogBuilder.Build(AssetDatabase.GetAssetPath(file));
                _dialogs.Add(tree);
            }
        }

        public DialogTree GetDialog(string name) => _dialogs.FirstOrDefault(x => x.Name == name);
    }

    public interface IDialogDataService
    {
        public DialogTree GetDialog(string name);
    }
}