using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScripter : MonoBehaviour
{
    public GameObject DeathSpriteChange;
    //--Переменные условий квестов
    public int DeathCountTarget;
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
        if (DeathCount >= DeathCountTarget)
        {
            DeathSpriteChange.GetComponent<IDeathQuester>().QuestDone();
        }
    }
}
