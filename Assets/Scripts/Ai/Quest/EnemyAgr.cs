using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgr : MonoBehaviour, IDeathQuester
{
    private GameObject parent;
    public GameObject[] Enemys;
    public bool QuestDone = false;
    private void Start()
    {
        parent = GameObject.Find("QusetTriggers");
    }
    public void QuestGo() {
        if (!QuestDone) {
            foreach (GameObject enemy in Enemys)
            {
                try
                {
                    enemy.gameObject.GetComponent<EnemyAI>().stop = false;
                }
                catch
                {
                }
            }
            QuestDone = true;
        }
    }
}
