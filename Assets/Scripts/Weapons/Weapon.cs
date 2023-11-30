using UnityEngine;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour, IInteractable
{
    [SerializeField] protected Ammo _ammoType;
    [SerializeField] protected string _desc;
    [SerializeField] protected string _name;
    [SerializeField] protected int _degreesInactiveRotation;
    [FormerlySerializedAs("_unactiveHoldPoint"), SerializeField] protected Transform _inactiveHoldPoint;
    [SerializeField] protected Transform _holdPoint;
    [SerializeField] protected int _maxAmmo;
    [SerializeField] protected int _currentAmmo;
    [FormerlySerializedAs("proj"),SerializeField] protected Projectile _proj;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Cooldown _fireCooldown;
    [SerializeField] protected float _reloadTime;
    [SerializeField] protected int _attackLayer;
    [SerializeField] protected Collider2D _collider;
    [SerializeField] public int _shootRange;
    public Ammo AmmoType => _ammoType;
    public int DegreesInactiveRotation => _degreesInactiveRotation;
    public Vector3 PivotLocalInactivePosHold => _inactiveHoldPoint.localPosition;
    public Vector3 PivotLocalPosHold => _holdPoint.localPosition;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public float ReloadTime => _reloadTime;
    public string Name => _name;
    public bool IsEmpty => _currentAmmo <= 0;
    public bool IsFull => _currentAmmo >= _maxAmmo;

    public int MaxAmmo => _maxAmmo;

    public InteractionType Id { get; set; } = InteractionType.Weapon;

    public string Description
    {
        get => _desc;
        set => _desc = value;
    }

    public void Drop(Vector3 at)
    {
        transform.SetParent(null);
        transform.position = at;
        _collider.enabled = true;
    }

    public void HandOnBack(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = -PivotLocalInactivePosHold;
        transform.localRotation = Quaternion.Euler(0, 0, _degreesInactiveRotation);
    }

    public void TakeUp(Transform parent, Vector3 localScale)
    {
        transform.SetParent(parent);
        transform.localPosition = -PivotLocalPosHold;
        transform.localRotation = Quaternion.identity;
        transform.localScale = localScale;
        _collider.enabled = false;
    }

    public virtual void ChangeSpriteOrder(int order)
    {
        SpriteRenderer.sortingOrder = order;
    }

    public void Interact(Character obj) => obj.TakeWeapon(this);

    public virtual void Attack()
    {
    }

    public virtual void Reload(int count) => _currentAmmo = count;
    public void SetAttackLayer(int layer) => _attackLayer = layer;
}
