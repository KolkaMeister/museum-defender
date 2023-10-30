using System.Collections.Generic;
using Dialogs.Nodes;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Dialogs
{
    public class DialogDataProvider : IInitializable, IDialogDataService
    {
        private readonly DialogSo _dialogSo;
        private readonly List<DialogTree> _dialogs = new List<DialogTree>();

        public DialogDataProvider(DialogSo dialogSo)
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

        public DialogTree Find(string name) => _dialogs.Find(x => x.Name == name);
        public List<DialogTree> FindAll(string template) => _dialogs.FindAll(x => x.Name.StartsWith(template));
    }

    public interface IDialogDataService
    {
        public DialogTree Find(string name);
        public List<DialogTree> FindAll(string template);
    }
}