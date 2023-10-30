using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] [CreateAssetMenu(fileName = "DialogData", menuName = "Defs/DialogData")]
public class DialogData : ScriptableObject
{
    [SerializeField] public DialogDataItem[] Dialogs;
    [SerializeField] public UnityEvent OnDialogEnd;

}

[Serializable]
public class DialogDataItem
{
     public string Name;
     public string Text;
}
