using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpriteChange : MonoBehaviour, IDeathQuester
{
    public GameObject targetObject;
    public Sprite NewSprite;
    public void QuestDone() {
        targetObject.GetComponent<SpriteRenderer>().sprite = NewSprite;
    }
}
