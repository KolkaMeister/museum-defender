using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSceneLoad : MonoBehaviour, IInteractable
{
    [SerializeField] string _desc;
    [SerializeField] int _loadSceneNumber;
    public string Description { get => _desc; set => _desc=value; }

    public void Interact(GameObject obj)
    {
        SceneLoadCanvas.Instance.LoadScene(_loadSceneNumber);
    }
}
