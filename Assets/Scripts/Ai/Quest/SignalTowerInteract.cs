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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact(Character obj) {
        gameObject.GetComponent<Animator>().SetBool("Enabaled", !GetComponent<Animator>().GetBool("Enabaled"));
        if (GetComponent<Animator>().GetBool("Enabaled")) {
            GameObject.Find("SignalTowerQuest").GetComponent<SignalTowerQuest>().interacted += 1;
            
        }
        else {
            GameObject.Find("SignalTowerQuest").GetComponent<SignalTowerQuest>().interacted -= 1;
        }
    }
}
