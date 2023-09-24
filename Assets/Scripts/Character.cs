using System.Collections;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utility;

public class Character : MonoBehaviour, ITakeDamage
{
    //**********************Interaction****************************//
    private ClassPersistantProperty<IInteractable> _interactionTarget = new ClassPersistantProperty<IInteractable>(null);
    [SerializeField] private GameObject _deadCond;

    //***********************Weapons*******************************//
    private readonly WeaponsInventory _weaponInventory = new WeaponsInventory();
    [SerializeField] private GameObject _weaponsHolder;
    [SerializeField] private AmmoInventoryData _ammoInventory = new AmmoInventoryData();
    [FormerlySerializedAs("holdPoint"),SerializeField] private Transform _holdPoint;
    [FormerlySerializedAs("backHoldPoint"),SerializeField] private Transform _backHoldPoint;

    //********************Physics***************************//
    [SerializeField] private Rigidbody2D _rb;
    [FormerlySerializedAs("_VelMulti"),SerializeField] private float _velMulti;
    [SerializeField] private Collider2D _collider;
    private Vector2 _moveDirection = new Vector2(0, 0);
    private Vector2 _aimPos = new Vector2(1, 1);

    [SerializeField] private PersistantProperty<float> _health = new PersistantProperty<float>(100);
    [SerializeField] private EnemyAI _ai;
    
    private Animator _animator;
    private static readonly int _isMovingKey = Animator.StringToHash("IsMoving");
    private static readonly int _deathKey = Animator.StringToHash("Death");


    private bool _isReloading;

    private bool _isDead;

    public Vector2 MoveDirection
    {
        get => _moveDirection;
        set => _moveDirection = value;
    }

    public Vector2 AimPos
    {
        get => _aimPos;
        set
        {
            _aimPos = value;
            CalculateScale(value);
            CalculateWeaponRotation(value);
        }
    }

    public PersistantProperty<float> Health
    {
        get => _health;
        set => _health = value;
    }

    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }

    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        TryGetComponent(out _collider);
        TryGetComponent(out _ai);
    }

    private void OnEnable()
    {
        _interactionTarget.OnChanged += OnInteractionTargetChanged;
        _weaponInventory.OnListChanged += OnInventoryChanged;
        _weaponInventory.OnUseChanged += OnInventoryIndexChanged;
        _weaponInventory.OnUseChanged += ReloadCheck;
        _health.OnChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _interactionTarget.OnChanged -= OnInteractionTargetChanged;
        _weaponInventory.OnListChanged -= OnInventoryChanged;
        _weaponInventory.OnUseChanged -= OnInventoryIndexChanged;
        _weaponInventory.OnUseChanged -= ReloadCheck;
        _health.OnChanged -= OnHealthChanged;
    }

    public void Update()
    {
        Velocity();
    }

    public void OnInteractionTargetChanged(IInteractable val)
    {
        if (val == null)
            return;
    }

    private void Velocity()
    {
        _rb.velocity = _moveDirection * _velMulti;
        _animator.SetBool(_isMovingKey, _rb.velocity != Vector2.zero);
    }

    public void Interact()
    {
        CheckInteraction();
        _interactionTarget.Value?.Interact(this);
    }

    private void CalculateScale(Vector2 view)
    {
        float dir = view.x - transform.position.x;
        transform.localScale = new Vector2(dir > 0 ? 1 : -1, 1);
    }

    private void CalculateWeaponRotation(Vector2 view)
    {
        Vector2 direction = view - (Vector2)_weaponsHolder.transform.position;
        float rad = Mathf.Atan(direction.y / direction.x);
        _weaponsHolder.transform.rotation = Quaternion.Euler(0, 0, rad * 180 / Mathf.PI);
    }

    private void CheckInteraction()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, 1, LayerMask.GetMask("Interactable"));
        _interactionTarget.Value = coll != null ? coll.GetComponent<IInteractable>() : null;
        // Debug.Log(coll?.name);
    }

    //////////Weapons Methods//////////
    public void TakeWeapon(Weapon _wep)
    {
        _weaponInventory.TakeWeapon(_wep);
        _wep.SetAttackLayer(gameObject.layer == Idents.PlayerLayer ? Idents.EnemyLayer : Idents.PlayerLayer);
    }

    public void OnInventoryChanged(Weapon _old, Weapon _new)
    {
        DropWeaponAtPoint(_old, transform.position);
        if (!_new) return;
        
        TakeUpWeapon(_new);
    }

    private void DropWeaponAtPoint(Weapon _wep, Vector3 _dropPos)
    {
        if (!_wep) return;
        
        _wep.transform.parent = null;
        _wep.transform.position = _dropPos;
        _wep.GetComponent<Collider2D>().enabled = true;
        _wep.SpriteRenderer.sortingOrder = 1;
    }

    public void SetCurrentWeaponIndex(int weaponIndex)
    {
        // Debug.Log(weaponIndex);
        _weaponInventory.ChangeIndex(weaponIndex);
    }

    public void OnInventoryIndexChanged(Weapon _current, Weapon _last)
    {
        if (_last)
        {
            HangOnBackWeapon(_last);
            // Debug.Log(_last);
        }

        if (_current)
        {
            TakeUpWeapon(_current);
            // Debug.Log(_current);
        }
    }

    private void HangOnBackWeapon(Weapon _wep)
    {
        _wep.gameObject.transform.parent = _backHoldPoint;
        _wep.transform.localPosition = -_wep.PivotLocalInactivePosHold;
        _wep.transform.localRotation = Quaternion.Euler(0, 0, _wep.degreesInactiveRotation);
        _wep.SpriteRenderer.sortingOrder = 1;
    }

    private void TakeUpWeapon(Weapon _wep)
    {
        if (!_wep) return;
        
        _wep.gameObject.transform.parent = _holdPoint.transform;
        _wep.transform.localPosition = -_wep.PivotLocalPosHold;
        _wep.transform.localRotation = Quaternion.Euler(0, 0, 0);
        _wep.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        _wep.SpriteRenderer.sortingOrder = 3;
        _wep.GetComponent<Collider2D>().enabled = false;
    }

    public void Attack()
    {
        if (!_weaponInventory.CurrentWeapon)
            return;
        if (_weaponInventory.CurrentWeapon.IsEmpty)
            Reload();
        else
            _weaponInventory.CurrentWeapon.Attack();
    }

    public void ReloadCheck(Weapon current, Weapon last)
    {
        if (current != last && _isReloading)
        {
            StopAllCoroutines();
            _isReloading = false;
        }
    }

    public void ReloadWeapon()
    {
        Reload();
    }

    private void Reload()
    {
        if (_isReloading) return;
        
        Weapon current = _weaponInventory.CurrentWeapon;
        if (!current || current.IsFull || current.ReloadTime == 0)
            return;
        if (_ammoInventory.GetAmmo(current.AmmoType) < 1)
            return;
        StartCoroutine(ReloadRoutine(current.ReloadTime));
    }

    private IEnumerator ReloadRoutine(float time)
    {
        _isReloading = true;
        yield return new WaitForSeconds(time);

        Weapon current = _weaponInventory.CurrentWeapon;
        if (!current) yield break;
        
        int totalCount = _ammoInventory.GetAmmo(current.AmmoType);
        int relCount = Mathf.Min(totalCount, current.MaxAmmo);
        current.Reload(relCount);
        _ammoInventory.ReduceAmmo(current.AmmoType, relCount);
        _isReloading = false;
    }

    public void TakeDamage(float value)
    {
        _health.Value -= value;
    }

    public void HealHealth(float value)
    {
        _health.Value += value;
    }

    public void OnHealthChanged(float newValue, float old)
    {
        if (newValue <= 0)
        {
            if (IsDead) return;
            
            IsDead = true;
            if (_collider)
                _collider.enabled = false;
            _animator.SetTrigger(_deathKey);
            DropWeapons();
            _moveDirection = Vector2.zero;
            if (!_ai)
                _ai.enabled = false;

            if (gameObject.name == "Player")
                SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex, false);
            Instantiate(_deadCond, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
            Debug.Log(newValue);
    }

    private void DropWeapons()
    {
        _weaponInventory.DropWeapon(0);
        _weaponInventory.DropWeapon(1);
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = new Color(1, 1, 0, 0.1f);
        Handles.DrawSolidDisc(transform.position, Vector3.forward, 1);
    }
#endif
}