using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Character : MonoBehaviour, ITakeDamage
{
    //**********************Interaction****************************//
    [SerializeField] private GameObject _deadCond;

    //***********************Weapons*******************************//
    private Inventory _inventory;

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
            _inventory.CalculateWeaponRotation(value);
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
        _inventory = GetComponent<Inventory>();
        TryGetComponent(out _collider);
        TryGetComponent(out _ai);
    }

    private void OnEnable()
    {
        _health.OnChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
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

    //////////Weapons Methods//////////
    public void TakeWeapon(Weapon wep) => _inventory.TakeWeapon(wep);
    public void SetCurrentWeaponIndex(int weaponIndex) => _inventory.SetCurrentWeaponIndex(weaponIndex);
    public void ReloadWeapon() => _inventory.Reload();

    public void Attack()
    {
        Weapon weapon = _inventory.CurrentWeapon;
        if (!weapon) return;
        if (weapon.IsEmpty)
            _inventory.Reload();
        else
            weapon.Attack();
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
            _inventory.DropAll();
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = new Color(1, 1, 0, 0.1f);
        Handles.DrawSolidDisc(transform.position, Vector3.forward, 1);
    }
#endif
}