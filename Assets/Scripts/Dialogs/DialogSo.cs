using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Dialogs
{
    [CreateAssetMenu(fileName = "Dialogs", menuName = "History Trip/Xml Dialogs")]
    public class DialogSo : ScriptableObject
    {
        public TextAsset[] XmlDialogs;

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < XmlDialogs.Length; i++)
            {
                if (Path.GetExtension(AssetDatabase.GetAssetPath(XmlDialogs[i])) != ".xml")
                {
                    XmlDialogs[i] = null;
                    throw new ArgumentException("Dialog must be an xml file");
                }
            }
        }
#endif
    }
}