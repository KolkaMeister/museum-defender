using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] protected bool _destroyOnHit;
    [SerializeField] protected float _modifyValue;

    [SerializeField] protected int _damageLayer;

    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected SpriteRenderer _renderer;

    public Collider2D Collider => _collider;
    public SpriteRenderer Renderer => _renderer;
    public Rigidbody2D Rb => _rb;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (_damageLayer == collision.gameObject.layer && collision.TryGetComponent(out ITakeDamage damage))
        {
            ModifyHealth(damage);
            if (_destroyOnHit)
                BackToPool();
        }
    }

    protected virtual void ModifyHealth(ITakeDamage obj)
    {
        obj.ChangeHealth(_modifyValue);
    }

    public virtual void ChangeLayer(int layer)
    {
        _renderer.sortingOrder = layer;
    }

    public virtual void Shot(Vector2 dir, float speed, int layer)
    {
    }

    protected virtual void BackToPool()
    {
    }
}