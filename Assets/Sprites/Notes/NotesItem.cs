using System;
using UnityEditor.U2D.Animation;
using UnityEngine;
[CreateAssetMenu(fileName = "NotesItem", menuName = "History Trip/NotesItem")]
public class NotesItem : ScriptableObject
{
    public Sprite Icon;
    public Sprite BigSprite;
    public string Description;
}
