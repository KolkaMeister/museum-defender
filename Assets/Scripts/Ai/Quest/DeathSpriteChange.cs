using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpriteChange : MonoBehaviour, IDeathQuester
{
    private GameObject parent;
    public GameObject targetObject;
    public bool QuestDone = false;

    public int TargetDeathCount;
    private void Start()
    {
        parent = GameObject.Find("QusetTriggers");
    }
    public void QuestGo() {
        if (parent.GetComponent<QuestScripter>().DeathCount >= 5 && !QuestDone) {
            targetObject.GetComponent<Animator>().SetBool("Open", true);
            QuestDone = true;
        }
    }
}
