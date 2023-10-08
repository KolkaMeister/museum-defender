using UnityEngine;

public class Arrow : Projectile
{
    // TODO: arrows does not come back to pool and are destroyed
    
    public override void Shot(Vector2 dir, float speed, int layer)
    {
        transform.SetParent(null);
        transform.localScale = Vector3.one;
        _rb.velocity = dir * speed;
        _collider.isTrigger = true;
        _collider.enabled = true;
        _damageLayer = layer;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (_damageLayer == collision.gameObject.layer && collision.TryGetComponent(out ITakeDamage damage))
        {
            ModifyHealth(damage);
            _rb.velocity = Vector2.zero;
            transform.SetParent(collision.transform);
        }
    }
}
