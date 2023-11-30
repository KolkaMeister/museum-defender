using UI;
using UnityEngine;

public class InteractSceneLoad : MonoBehaviour, IInteractable
{
    [SerializeField] private string _desc;
    [SerializeField] private int _loadSceneNumber;

    public InteractionType Id { get; set; } = InteractionType.Scene;

    public string Description
    {
        get => _desc;
        set => _desc = value;
    }

    public void Interact(Character _)
    {
        SceneLoader.LoadScene(_loadSceneNumber, false);
    }
}