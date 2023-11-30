using UnityEngine;

public class Arrow : Projectile
{
    public override void Shot(Vector2 dir, float speed, int layer)
    {
        transform.SetParent(null);
        transform.localScale = Vector3.one;
        transform.right = dir;
        _rb.velocity = dir * speed;
        _collider.isTrigger = true;
        _collider.enabled = true;
        _damageLayer = layer;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (_damageLayer != collision.gameObject.layer) 
            return;
        
        _rb.velocity = Vector2.zero;
        transform.SetParent(collision.transform);
        if(collision.TryGetComponent(out ITakeDamage damage))
            ModifyHealth(damage);
    }
}
