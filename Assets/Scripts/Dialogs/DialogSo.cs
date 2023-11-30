using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Dialogs
{
    [CreateAssetMenu(fileName = "Dialogs", menuName = "History Trip/Xml Dialogs")]
    public class DialogSo : ScriptableObject
    {
        public string[] FileNames;
        
#if UNITY_EDITOR
        public TextAsset[] XmlDialogs;
        
        private void OnValidate()
        {
            FileNames = new string [XmlDialogs.Length];
            for (int i = 0; i < XmlDialogs.Length; i++)
            {
                if (Path.GetExtension(AssetDatabase.GetAssetPath(XmlDialogs[i])) != ".xml")
                {
                    XmlDialogs[i] = null;
                    throw new ArgumentException("Dialog must be an xml file");
                }

                FileNames[i] = Path.GetFileName(AssetDatabase.GetAssetPath(XmlDialogs[i]));
            }
        }
#endif
    }
}