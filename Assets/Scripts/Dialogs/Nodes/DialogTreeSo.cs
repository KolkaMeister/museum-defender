using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialogs.Nodes
{
    [CreateAssetMenu(fileName = "NewDialog", menuName = "History Trip/Dialog Tree")]
    public class DialogTreeSo : ScriptableObject
    {
#if UNITY_EDITOR
        public TextAsset XmlFile;
        public List<DialogNode> Dialogs;

        public void BuildDialogTree()
        {
            Root = DialogBuilder.Build(AssetDatabase.GetAssetPath(XmlFile)).Root;
            Dialogs = Root.GetAllChildrenAndSelf();
        }
#endif

        public DialogNode Root;
    }
}