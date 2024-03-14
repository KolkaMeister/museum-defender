using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interaction : MonoBehaviour, IInteraction
{
    public bool IsUpdate = true;
    public float UpdateTime;
    public float InteractionDistance;
    
    private readonly List<IInteractable> _targets = new List<IInteractable>();
    private bool _isInitialized;
    private Character _character;
    
#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] private MonoBehaviour[] _debugTargets;

    private void Update()
    {
        _debugTargets = _targets.OfType<MonoBehaviour>().ToArray();
    }
#endif
    
    public void Interact(InteractionType id)
    {
        if (!_isInitialized) 
            InternalCheck();

        IInteractable target = _targets.Find(x => x.Id == id);
        target?.Interact(_character);
    }
    
    public void Interact()
    {
        if (!_isInitialized) 
            InternalCheck();
        
        if(_targets.Count > 0)
            _targets[0].Interact(_character);
    }

    public void CheckInteraction()
    {
        StopCoroutine(UpdateTarget());
        StartCoroutine(UpdateTarget());
    }

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void Start()
    {
        StartCoroutine(UpdateTarget());
    }

    private IEnumerator UpdateTarget()
    {
        while (IsUpdate)
        {
            InternalCheck();
            _isInitialized = true;
            yield return new WaitForSeconds(UpdateTime);
        }
    }

    private void InternalCheck()
    {
        _targets.Clear();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, InteractionDistance);
        foreach (Collider2D coll in colliders)
        {
            if (coll.TryGetComponent(out IInteractable target))
                _targets.Add(target);
        }
    }
}

public interface IInteraction
{
    public void Interact();
    public void Interact(InteractionType id);
    public void CheckInteraction();
}

public enum InteractionType
{
    Dialog,
    Weapon,
    Scene
}
