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
        ProgressString = "Сигнальный огонь: \n";
        ProgressString += " Зажечь вышки: " + interacted + " / " + targetActive;
    }
    new public void QuestGo() {
        //Debug.Log("Зажженых вышек: " + interacted);
        ProgressString = "Сигнальный огонь: \n";
        ProgressString += " Зажечь вышки: " + interacted + " / " + targetActive;
        if (interacted >= targetActive) {
            Debug.Log("Вышки зажжены");
            QuestDone = true;
            ProgressString = "Вышки зажжены. Вернитесь к Князю.";
        }
        GameObject.Find("QuestViewText").GetComponent<QuestView>().UpdateQuestText();
    }
}
