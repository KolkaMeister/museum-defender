using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTowerInteract : MonoBehaviour, IInteractable
{
    public InteractionType Id { get; set; } = InteractionType.Object;

    public string Description { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("QuestViewText").GetComponent<QuestView>().UpdateQuestText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact(Character obj) {
        try {
            bool a = GameObject.Find("SignalTowerQuest").activeSelf;
        } catch { 
            Debug.Log("Что-то не так");
            return;
        }
        gameObject.GetComponent<Animator>().SetBool("Enabaled", !GetComponent<Animator>().GetBool("Enabaled"));
        if (GetComponent<Animator>().GetBool("Enabaled")) {
            try { 
                GameObject.Find("SignalTowerQuest").GetComponent<SignalTowerQuest>().interacted += 1; 
            } catch { 
                Debug.LogWarning("Что-то не так"); 
            }
            
        }
        else {
            try { GameObject.Find("SignalTowerQuest").GetComponent<SignalTowerQuest>().interacted -= 1; } catch { Debug.LogWarning("Что-то не так"); }
        }
    }
}
