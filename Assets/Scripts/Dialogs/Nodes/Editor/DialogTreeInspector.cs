using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialogs.Nodes
{
    [CustomEditor(typeof(DialogTreeSo))]
    public class DialogTreeInspector : Editor
    {
        private bool _isFoldout;
        private SerializedProperty _dialogsProp;
        [SerializeField] private List<DialogNode> _dialogs;
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("XmlFile"));
            DialogTreeSo me = (DialogTreeSo)target;
            if (GUILayout.Button("Build Tree"))
            {
                me.BuildDialogTree();
            }

            _dialogsProp = serializedObject.FindProperty("Root");
            EditorGUILayout.PropertyField(_dialogsProp, true);
            
            _dialogs = me.Root.GetAllChildrenAndSelf();
            var obj = new SerializedObject(this);
            SerializedProperty props = obj.FindProperty("_dialogs");
            GUIStyle style = new GUIStyle();
            style.padding = new RectOffset(50, 0, 0, 0);
            _isFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(_isFoldout, "Dialogs", EditorStyles.foldoutHeader);
            
            if(_isFoldout)
            {
                foreach (SerializedProperty prop in props)
                {
                    EditorGUILayout.PropertyField(prop);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}