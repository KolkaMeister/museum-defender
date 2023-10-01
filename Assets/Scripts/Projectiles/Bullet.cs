using UnityEngine;

public class Bullet : Projectile
{
    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    public override void Shot(Vector3 dir, float speed, int layer)
    {
        _rb.velocity = dir * speed;
        _damageLayer = layer;
    }
}
