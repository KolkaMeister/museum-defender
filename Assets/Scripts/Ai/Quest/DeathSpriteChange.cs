using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpriteChange : MonoBehaviour, IDeathQuester
{
    private GameObject parent;
    public GameObject targetObject;

    public int TargetDeathCount;
    private void Start()
    {
        parent = GameObject.Find("QusetTriggers");
    }
    public void QuestGo() {
        if (parent.GetComponent<QuestScripter>().DeathCount >= 5) {
            targetObject.GetComponent<Animator>().SetBool("Open", true);
            Debug.Log("Quest");
        }
    }
}
