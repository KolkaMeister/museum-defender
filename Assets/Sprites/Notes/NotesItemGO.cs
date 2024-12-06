using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesItemGO : MonoBehaviour
{
    [SerializeField]
    NotesItem notesItem;
    [SerializeField]
    NotesUI notesUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Character>() !=null)
        {
            notesUI.AddItem(notesItem);
            Debug.Log($"Предмет { notesItem.name } собран");
            Destroy(gameObject);
        }
    }
}
