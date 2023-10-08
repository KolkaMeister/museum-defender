using UnityEngine;

public class Bullet : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    public override void Shot(Vector3 dir, float speed, int layer)
    {
        _rb.velocity = dir * speed;
        _damageLayer = layer;
    }
}
