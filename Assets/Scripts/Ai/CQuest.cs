using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CQuest : MonoBehaviour
{
    public bool QuestDone = false;
    public int progress = 0;
    public int MaxProgress  = 4;
    public Action OnCompleted { get; internal set; }
    [ContextMenu("CompleteQuest")]
    public void CompleteQuest()
    {
        for(int i = 0;i<100; i++)
        {
            AddProgress(1);
            if (QuestDone)
                break;
        }
    }
    internal void AddProgress(int v)
    {
        progress += 1;
        if(progress == MaxProgress)
        {
            OnProgressCompleted();
        }
    }

    private void OnProgressCompleted()
    {
        QuestDone = true;
        OnCompleted?.Invoke();
    }

    public void QuestGo() {

        AddProgress(1);
    }
    public string ProgressString = "";
}
