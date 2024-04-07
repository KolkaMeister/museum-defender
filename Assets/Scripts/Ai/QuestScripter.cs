using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScripter : MonoBehaviour
{
    public GameObject[] DeathCountQuests;
    //--���������� ������� �������
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
        foreach (GameObject Quest in DeathCountQuests)
        {
            Quest.GetComponent<IDeathQuester>().QuestGo();
        }
    }
    public void TriggerScript(string name)
    {
        try {
            GameObject.Find("QusetTriggers").transform.Find(name).gameObject.GetComponent<IDeathQuester>().QuestGo();
        } catch {
            Debug.LogWarning("�� ������ ����� ������ ����� ����� � ������: " + name);
        }
        
    }
}
