using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScripter : MonoBehaviour
{
    public GameObject GateQuest;
    //--Переменные условий квестов
    private int _deathCount;
    public int DeathCount
    {
        get
        {
            return _deathCount;
        }
        set
        {
            _deathCount = value;
            DeathCountScript();
        }
    }
    
    private void DeathCountScript() {
        GateQuest.GetComponent<IDeathQuester>().QuestGo();
    }
}
