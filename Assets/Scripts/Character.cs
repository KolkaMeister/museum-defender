using System.Collections;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utility;
using static Utility.Idents;

public class Character : MonoBehaviour, ITakeDamage
{
    //**********************Interaction****************************//
    [SerializeField] private GameObject _deadCond;

    //***********************Weapons*******************************//
    private readonly WeaponsInventory _weaponInventory = new WeaponsInventory();
    [SerializeField] private GameObject _weaponsHolder;
    [SerializeField] private AmmoInventoryData _ammoInventory = new AmmoInventoryData();

    [FormerlySerializedAs("holdPoint"), SerializeField]
    private Transform _holdPoint;

    [FormerlySerializedAs("backHoldPoint"), SerializeField]
    private Transform _backHoldPoint;

    //********************Physics***************************//
    [SerializeField] private Rigidbody2D _rb;

    [FormerlySerializedAs("_VelMulti"), SerializeField]
    private float _velMulti;

    private Interaction _interaction;
    private Collider2D _collider;
    private Vector2 _moveDirection = new Vector2(0, 0);
    private Vector2 _aimPos = new Vector2(1, 1);

    [SerializeField] private PersistantProperty<float> _health = new PersistantProperty<float>(100);
    private EnemyAI _ai;

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
        _interaction = GetComponent<Interaction>();
        TryGetComponent(out _collider);
        TryGetComponent(out _ai);
    }

    private void OnEnable()
    {
        _weaponInventory.OnListChanged += OnInventoryChanged;
        _weaponInventory.OnUseChanged += OnInventoryIndexChanged;
        _weaponInventory.OnUseChanged += ReloadCheck;
        _health.OnChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _weaponInventory.OnListChanged -= OnInventoryChanged;
        _weaponInventory.OnUseChanged -= OnInventoryIndexChanged;
        _weaponInventory.OnUseChanged -= ReloadCheck;
        _health.OnChanged -= OnHealthChanged;
    }

    public void Update()
    {
        Velocity();
    }

    private void Velocity()
    {
        _rb.velocity = _moveDirection * _velMulti;
        _animator.SetBool(_isMovingKey, _rb.velocity != Vector2.zero);
    }

    public void Interact() => _interaction.Interact();

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

    //////////Weapons Methods//////////
    public void TakeWeapon(Weapon wep)
    {
        _weaponInventory.TakeWeapon(wep);
        wep.SetAttackLayer(gameObject.layer == Layers.Player ? Idents.Layers.Enemy : Layers.Player);
    }

    private void OnInventoryChanged(Weapon oldValue, Weapon newValue)
    {
        if (oldValue) DropWeaponAtPoint(oldValue, transform.position);
        if (newValue) TakeUpWeapon(newValue);
    }

    private void DropWeaponAtPoint(Weapon wep, Vector3 position)
    {
        wep.Drop(position);
    }

    public void SetCurrentWeaponIndex(int weaponIndex)
    {
        // Debug.Log(weaponIndex);
        _weaponInventory.ChangeIndex(weaponIndex);
    }

    private void OnInventoryIndexChanged(Weapon current, Weapon last)
    {
        if (last)
        {
            HangOnBackWeapon(last);
            // Debug.Log(_last);
        }

        if (current)
        {
            TakeUpWeapon(current);
            // Debug.Log(_current);
        }
    }

    private void HangOnBackWeapon(Weapon wep)
    {
        wep.HandOnBack(_backHoldPoint);
    }

    private void TakeUpWeapon(Weapon wep)
    {
        wep.TakeUp(_holdPoint.transform, new Vector3(1, transform.localScale.y, transform.localScale.z));
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

    private void ReloadCheck(Weapon current, Weapon last)
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

    public void ChangeHealth(float value)
    {
        _health.Value += value;
    }

    private void OnHealthChanged(float newValue, float old)
    {
        if (newValue <= 0)
        {
            if (IsDead) return;

            IsDead = true;
            if (_collider) _collider.enabled = false;
            _animator.SetTrigger(_deathKey);
            DropWeapons();
            _moveDirection = Vector2.zero;
            if (_ai) _ai.enabled = false;

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