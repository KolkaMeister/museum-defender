using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTowerQuest : MonoBehaviour
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
    void QuestGo() {
        Debug.Log("Зажженых вышек: " + interacted);
        if (interacted >= targetActive) {
            Debug.Log("Вышки зажжены");
            QuestDone = true;
        }
    }
}
