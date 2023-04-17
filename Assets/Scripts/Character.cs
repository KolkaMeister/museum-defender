using TMPro;
using UnityEditor;
using UnityEngine;
using static ITakeDamage;

public class Character : MonoBehaviour, ITakeDamage
{
    //**********************Interaction****************************//
    private PersistantProperty<IInteractable> _interactionTarget=new PersistantProperty<IInteractable>(null);
    //***********************Weapons*******************************//
    [SerializeField] private GameObject _weaponsHolder;
    private WeaponsInventory _weaponInventory=new WeaponsInventory();
    private AmmoInventoryData _ammoInventory = new AmmoInventoryData();
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Transform backHoldPoint;
    //********************Physics***************************//
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _VelMulti;
    private Vector2 _moveDirection = new Vector2(0,0);
    private Vector2 _aimPos = new Vector2(1,1);

    public event healthChange OnChange;

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

    [SerializeField] private float _health;
    public float Health { get => _health; set =>_health=value ; }

    public void Start()
    {
        _interactionTarget.OnChanged += OnInteractionTargetChanged;
        _weaponInventory.OnListChanged += OnInventoryChange;
        _weaponInventory.OnUseChanged += OnInventoryIndexChange;
    }
    public void OnDestroy()
    {
        _interactionTarget.OnChanged -= OnInteractionTargetChanged;
        _weaponInventory.OnListChanged -= OnInventoryChange;
        _weaponInventory.OnUseChanged -= OnInventoryIndexChange;
    }
    public void Update()
    {
        Velocty();
        CheckInteraction();
    }
    public void OnInteractionTargetChanged(IInteractable val)
    {
        if (val == null)
            return;
        Debug.Log(val.Description);
    }
    private void Velocty()
    {
        _rb.velocity = _moveDirection * _VelMulti;
    }
    public void Interact()
    {
        if (_interactionTarget.Value!=null)
            _interactionTarget.Value.Interact(gameObject);
    }
    private void CalculateScale(Vector2 val)
    {
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
    }
    public void OnInventoryChange(Weapon _old, Weapon _new)
    {
        DropWeapon(_old);
        _new.GetComponent<Collider2D>().enabled = false;
        TakeUpWeapon(_new);
    }
    private void DropWeapon(Weapon _wep)
    {
        if (_wep == null)
            return;
        _wep.transform.parent = null;
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
        _wep.transform.localPosition = new Vector3(0, 0, 0);
        _wep.transform.localRotation = Quaternion.Euler(0, 0, _wep.DegreesUnactivRotation);
        _wep.SpriteRenderer.sortingOrder = 1;
    }
    private void TakeUpWeapon(Weapon _wep)
    {
        _wep.gameObject.transform.parent = holdPoint.transform;
        _wep.transform.localPosition =-_wep.PivotLocalPosHold;
        _wep.transform.localRotation = Quaternion.Euler(0,0,0);
        _wep.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        _wep.SpriteRenderer.sortingOrder = 3;
        _wep.GetComponent<Collider2D>().enabled = false;
    }
    public void ReloadWeapon()
    {
    }
    public void Attack()
    {
        _weaponInventory.CurrentWeapon.Attack();
    }
    public void TakeDamage(float value)
    {
        
    }

    public void HealHealth(float value)
    {
        throw new System.NotImplementedException();
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = new Color(1,1,0,0.1f);
        Handles.DrawSolidDisc(transform.position, Vector3.forward, 1);
    }

#endif
}
