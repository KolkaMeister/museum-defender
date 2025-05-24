using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesItemGO : MonoBehaviour
{
    [SerializeField]
    NotesItem notesItem;
    private SpriteRenderer _image;
    private SpriteRenderer Image { get {
            if (_image == null)
                _image = GetComponent<SpriteRenderer>();
            return _image; } }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Character character = other.GetComponent<Character>();
        if (character!=null)
        {
            character.AddItem(notesItem);
            Debug.Log($"Предмет { notesItem.name } собран");
            Destroy(gameObject);
        }
    }
    private void OnValidate()
    {
        if (Image != null && notesItem != null)
        {
            Image.sprite = notesItem.BigSprite;
        }
    }
}