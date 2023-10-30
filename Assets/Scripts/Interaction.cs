using System.Collections;
using UnityEngine;

public class Interaction : MonoBehaviour, IInteraction
{
    public bool IsUpdate = true;
    public float UpdateTime;
    public float InteractionDistance;
    
    private bool _isInitialized;
    private Character _character;
    private IInteractable _target;
    
#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] private MonoBehaviour _debugTarget;

    private void Update()
    {
        _debugTarget = _target as MonoBehaviour;
    }
#endif
    
    public void Interact()
    {
        if (!_isInitialized) 
            InternalCheck();
        _target?.Interact(_character);
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
        _target = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, InteractionDistance);
        foreach (Collider2D coll in colliders)
        {
            if (coll.TryGetComponent(out _target))
                break;
        }
    }
}

public interface IInteraction
{
    public void Interact();
    public void CheckInteraction();
}
