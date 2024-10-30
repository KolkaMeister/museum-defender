using System;
using System.Collections;
using Dialogs.Sideline;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Character : MonoBehaviour, ITakeDamage
{
    public EntityType Id;

    //**********************Interaction****************************//
    [SerializeField] private GameObject _deadCond;

    //***********************Weapons*******************************//
    private Inventory _inventory;

    //********************Physics***************************//
    [SerializeField] private Rigidbody2D _rb;

    [FormerlySerializedAs("_VelMulti"), SerializeField]
    private float _velMulti;
    [SerializeField] private float _pushForce;

    //********************Dash********************//
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private Cooldown _dashDelay;

    //*******************Dialog*******************//
    [SerializeField] private BubbleDialogView _dialogView;

    private Interaction _interaction;
    private Collider2D _collider;
    private Vector2 _moveDirection = new Vector2(0, 0);
    private Vector2 _aimPos = new Vector2(1, 1);

    [SerializeField] private PersistantProperty<float> _health = new PersistantProperty<float>(100);
    private EnemyAI _ai;

    private Animator _animator;
    private static readonly int _isMovingKey = Animator.StringToHash("IsMoving");
    private static readonly int _deathKey = Animator.StringToHash("Death");
    private static readonly int _isDashingKey = Animator.StringToHash("IsDashing");


    private bool _isReloading;
    private bool _isDead;
    private bool _isDash;
    private Vector3 _deadImpulse;

    public BubbleDialogView DialogView => _dialogView;

    public Rigidbody2D Rb => _rb;

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
        if (_dialogView)
            _dialogView.SetActive(false);
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
        if (_isDash) return;
        _rb.velocity = _moveDirection * _velMulti;
        _animator.SetBool(_isMovingKey, _rb.velocity != Vector2.zero);
    }

    public void Interact(InteractionType type) => _interaction.Interact(type);
    public void Interact() => _interaction.Interact();

    private void CalculateScale(Vector2 view)
    {
        float dir = view.x - transform.position.x;
        var scale = new Vector3(dir > 0 ? 1 : -1, 1, 1);
        transform.localScale = scale;
        if (_dialogView)
            _dialogView.SetScale(scale);
    }

    public void Dash()
    {
        if (_isDash || !_dashDelay.IsReady) return;
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        _isDash = true;
        _rb.velocity = _moveDirection * _dashSpeed;
        _animator.SetBool(_isDashingKey, true);
        yield return new WaitForSeconds(_dashTime);
        _animator.SetBool(_isDashingKey, false);
        _isDash = false;
        _dashDelay.Reset();
    }

    //////////Weapons Methods//////////
    public void TakeWeapon(Weapon wep) => _inventory.TakeWeapon(wep);
    public void SetCurrentWeaponIndex(int weaponIndex) => _inventory.SetCurrentWeaponIndex(weaponIndex);
    public void ReloadWeapon() => _inventory.Reload();

    public void Attack()
    {
        if (_isDash) return;

        Weapon weapon = _inventory.CurrentWeapon;
        if (!weapon) return;
        if (weapon.IsEmpty)
            _inventory.Reload();
        else
            weapon.Attack();
    }

    public void AddHealth(float value)
    {
        _health.Value += value;
    }

    public void Push(Vector3 origin)
    {
        var dir = (transform.position - origin).normalized;
        _deadImpulse = dir * _pushForce;
    }

    private void OnHealthChanged(float newValue, float old)
    {
        if (newValue <= 0)
        {
            if (IsDead) return;

            IsDead = true;
            if (_collider)
                _collider.enabled = false;
            _animator.SetTrigger(_deathKey);
            _inventory.DropAll();
            _moveDirection = Vector2.zero;
            if (_ai)
                _ai.enabled = false;

            if (Id == EntityType.Player)
                SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex, false);
            var dead = Instantiate(_deadCond, transform.position, Quaternion.identity).GetComponent<DeadCharacter>();
            dead.SetImpulse(_deadImpulse);
            DeathCounter();
            Destroy(gameObject);
        }
    }

    private void DeathCounter() //Добавить +1 к счётчику убийств
    {
        if (this.tag == "Enemies") {
            GameObject quest;
            try
            {
                quest = GameObject.Find("QuestTriggers");
                quest.GetComponent<QuestScripter>().DeathCount++;
            }
            catch(Exception e)
            {
                Debug.LogWarning(e.Message);
                return;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = new Color(1, 1, 0, 0.1f);
        Handles.DrawSolidDisc(transform.position, Vector3.forward, 1);
    }

    private void OnValidate()
    {
        _dashSpeed = _dashTime == 0 ? 0 : _dashDistance / _dashTime;
    }
#endif
}