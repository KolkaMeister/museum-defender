using System.Collections;
using UnityEngine;
using Utility;

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
    [SerializeField] private MonoBehaviour DebugTarget;

    private void Update()
    {
        DebugTarget = _target as MonoBehaviour;
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
        Collider2D coll = Physics2D.OverlapCircle(transform.position, InteractionDistance, LayerMask.GetMask("Interactable"));
        _target = coll != null ? coll.GetComponent<IInteractable>() : null;
    }
}

public interface IInteraction
{
    public void Interact();
    public void CheckInteraction();
}
