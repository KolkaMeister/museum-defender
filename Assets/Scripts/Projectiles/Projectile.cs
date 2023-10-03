using System;
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
                DestroyOnHit();
        }
    }

    protected virtual void ModifyHealth(ITakeDamage obj)
    {
        obj.ChangeHealth(_modifyValue);
    }

    public virtual void AddForce(Vector3 dir, float speed)
    {
        transform.rotation = Quaternion.Euler(0, 0, MathF.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        //Debug.Log(_dir);
        //_rb.velocity= new Vector2(Mathf.Cos(transform.rotation.z), Mathf.Sin(transform.rotation.z)) * _multi;
        _rb.velocity = transform.right * speed;
    }

    public virtual void Shot(Vector3 dir, float speed, int layer)
    {
    }

    private void DestroyOnHit()
    {
        Destroy(gameObject);
    }
}