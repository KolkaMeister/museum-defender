using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTowerQuest : CQuest
{
    public int targetActive = 4;
    public bool QuestDone = false;
    private int _interacted = 0;
    
    public int interacted {
        get
        {
            return _interacted;
        }
        set {
            _interacted = value;
            QuestGo();
        }
    }
    private void Awake()
    {
        ProgressString = "���������� �����: \n";
        ProgressString += " ������ �����: " + interacted + " / " + targetActive;
    }
    new public void QuestGo() {
        //Debug.Log("�������� �����: " + interacted);
        ProgressString = "���������� �����: \n";
        ProgressString += " ������ �����: " + interacted + " / " + targetActive;
        if (interacted >= targetActive) {
            Debug.Log("����� �������");
            QuestDone = true;
            ProgressString = "����� �������. ��������� � �����.";
        }
        GameObject.Find("QuestViewText").GetComponent<QuestView>().UpdateQuestText();
    }
}
