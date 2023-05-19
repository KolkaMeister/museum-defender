using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ITakeDamage;

public class Character : MonoBehaviour, ITakeDamage
{
    //**********************Interaction****************************//
    private ClassPersistantProperty<IInteractable> _interactionTarget = new ClassPersistantProperty<IInteractable>(null);
    //***********************Weapons*******************************//
    //Удалить
    [SerializeField] private GameObject _colhozSmert;
    //Удалить
    [SerializeField] private GameObject _weaponsHolder;
    private WeaponsInventory _weaponInventory = new WeaponsInventory();
    [SerializeField] private AmmoInventoryData _ammoInventory = new AmmoInventoryData();
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Transform backHoldPoint;
    //********************Physics***************************//
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _VelMulti;
    private Vector2 _moveDirection = new Vector2(0, 0);
    private Vector2 _aimPos = new Vector2(1, 1);

    private Animator _animator;
    static readonly int IsMovingKey = Animator.StringToHash("IsMoving");
    static readonly int DeathKey = Animator.StringToHash("Death");
    public Vector2 MoveDirection {
        get { return _moveDirection; }
        set
        {
            _moveDirection = value;
        }
    }
    public Vector2 AimPos {
        get { return _aimPos; }
        set
        {
            _aimPos = value;
            CalculateScale(value);
            CalculateWeaponRotation(value);
        }
    }

    private Coroutine _reloadRoutine;

    [SerializeField] public PersistantProperty<float> _health = new PersistantProperty<float>(100);
    private bool _isDead = false;
    public PersistantProperty<float> Health { get => _health; set => _health = value; }

    public bool IsDead  { get => _isDead; set => _isDead = value; }

    public void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        _interactionTarget.OnChanged += OnInteractionTargetChanged;
        _weaponInventory.OnListChanged += OnInventoryChange;
        _weaponInventory.OnUseChanged += OnInventoryIndexChange;
        _weaponInventory.OnUseChanged+=ReloadCheck;
        _health.OnChanged += OnHealthChanged;
    }
    public void OnDestroy()
    {
        _interactionTarget.OnChanged -= OnInteractionTargetChanged;
        _weaponInventory.OnListChanged -= OnInventoryChange;
        _weaponInventory.OnUseChanged -= OnInventoryIndexChange;
        _weaponInventory.OnUseChanged -= ReloadCheck;
        _health.OnChanged -= OnHealthChanged;
    }
    public void Update()
    {
        Velocty();
    }
    public void OnInteractionTargetChanged(IInteractable val)
    {
        if (val == null)
            return;
    }
    private void Velocty()
    {
        _rb.velocity = _moveDirection * _VelMulti;
        if (_rb.velocity != Vector2.zero)
            _animator.SetBool(IsMovingKey, true);
        else
            _animator.SetBool(IsMovingKey, false);
    }
    public void Interact()
    {
        CheckInteraction();
        if (_interactionTarget.Value != null)
            _interactionTarget.Value.Interact(gameObject);
    }
    private void CalculateScale(Vector2 val)
    {
        //Debug.Log(this);
        var dir = (val.x - transform.position.x);
        if ((val.x-transform.position.x) < 0) { transform.localScale = new Vector2(-1, 1); }
        if ((val.x - transform.position.x) > 0) { transform.localScale = new Vector2(1, 1); }
    }
    private void CalculateWeaponRotation(Vector2 val)
    {
        var direction = val - new Vector2(_weaponsHolder.transform.position.x, _weaponsHolder.transform.position.y);
        var rad = Mathf.Atan(direction.y/direction.x);
        _weaponsHolder.transform.rotation= Quaternion.Euler(0,0,(180/Mathf.PI)*rad);
    }
    private void CheckInteraction()
    {
        var coll = Physics2D.OverlapCircle(transform.position, 1, LayerMask.GetMask("Interactable"));
        _interactionTarget.Value = coll!=null?coll.GetComponent<IInteractable>():null;
       // Debug.Log(coll?.name);
    }
    //////////Weapons Methods//////////
    public void TakeWeapon(Weapon _wep)
    {
        _weaponInventory.TakeWeapon(_wep);
        _wep.SetAttackLayer();
    }
    public void OnInventoryChange(Weapon _old, Weapon _new)
    {
        DropWeaponAtPoint(_old,transform.position);
        if (_new == null)
            return;
        _new.GetComponent<Collider2D>().enabled = false;
        TakeUpWeapon(_new);
    }

    private void DropWeaponAtPoint(Weapon _wep,Vector3 _dropPos)
    {
        if (_wep == null)
            return;
        _wep.transform.parent = null;
        _wep.transform.position = _dropPos;
        _wep.GetComponent<Collider2D>().enabled = true;
        _wep.SpriteRenderer.sortingOrder = 1;
    }
    public void SetCurrentWeaponIndex(int val)
    {
        Debug.Log(val);
        _weaponInventory.ChangeIndex(val);
    }
    public void OnInventoryIndexChange(Weapon _current, Weapon _last)
    {
        if (_last != null)
        {
            HangOnBackWeapon(_last);
            Debug.Log(_last);
        }
        if (_current!=null)
        {
            TakeUpWeapon(_current);
            Debug.Log(_current);
        }
    }
    private void HangOnBackWeapon(Weapon _wep)
    {
        _wep.gameObject.transform.parent = backHoldPoint;
        _wep.transform.localPosition = -_wep.PivotLocalUnactivePosHold;
        _wep.transform.localRotation = Quaternion.Euler(0, 0, _wep.DegreesUnactivRotation);
        _wep.SpriteRenderer.sortingOrder = 1;
    }
    private void TakeUpWeapon(Weapon _wep)
    {
        if (_wep == null)
            return;
        _wep.gameObject.transform.parent = holdPoint.transform;
        _wep.transform.localPosition =-_wep.PivotLocalPosHold;
        _wep.transform.localRotation = Quaternion.Euler(0,0,0);
        _wep.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        _wep.SpriteRenderer.sortingOrder = 3;
        _wep.GetComponent<Collider2D>().enabled = false;
    }
    public void Attack()
    {
        if (_weaponInventory.CurrentWeapon == null)
            return;
        if (_weaponInventory.CurrentWeapon.IsEmpty&& _reloadRoutine==null)
        {
            Reload();
            return;
        }
        _weaponInventory.CurrentWeapon.Attack();
    }
    public void ReloadCheck(Weapon current, Weapon last)
    {
        if (current != last&& _reloadRoutine!=null)
        {
        StopCoroutine(_reloadRoutine);
        _reloadRoutine = null;
        }
    }
    public void ReloadWeapon()
    {
        if ( _reloadRoutine == null)
        Reload();
    }
    private void Reload()
    {
        if (_weaponInventory.CurrentWeapon == null)
            return;
        if (_weaponInventory.CurrentWeapon.IsFull)
            return;
        if (_weaponInventory.CurrentWeapon.ReloadTime == 0)
            return;
        if (_ammoInventory.CheckAmmo(_weaponInventory.CurrentWeapon.AmmoType) < 1)
            return;
        _reloadRoutine = StartCoroutine(ReloadRoutine(_weaponInventory.CurrentWeapon.ReloadTime));
    }

    private IEnumerator ReloadRoutine(float _time)
    {
        var value = 0f;

        while (value<_time)
        {
            value += Time.deltaTime;
            yield return null;
        }
        if (_weaponInventory.CurrentWeapon == null)
            yield break;
        var Totalcount = _ammoInventory.CheckAmmo(_weaponInventory.CurrentWeapon.AmmoType);
        var relCount = Mathf.Min(Totalcount, _weaponInventory.CurrentWeapon.MaxAmmo);
        _weaponInventory.CurrentWeapon.Reload(relCount);
        _ammoInventory.GetAmmo(_weaponInventory.CurrentWeapon.AmmoType, relCount);
        _reloadRoutine = null;
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
            if (IsDead)
                return;
            IsDead = true;
            var coll = GetComponent<Collider2D>();
            if (coll != null)
                coll.enabled = false;
            _animator.SetTrigger(DeathKey);
            DropWeapons();
            _moveDirection= Vector3.zero;
            var ai = GetComponent<EnemyAI>();
            if (ai != null)
                ai.enabled = false;
            //Удалить
            Instantiate(_colhozSmert,transform.position, Quaternion.identity);
            Destroy(gameObject);
            //Удалить
            if (gameObject.name == "Player")
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        Handles.color = new Color(1,1,0,0.1f);
        Handles.DrawSolidDisc(transform.position, Vector3.forward, 1);
    }
    
#endif
}
