using UnityEngine;

public class Arrow : Projectile
{
    private float delay = 0.4f;
    public AudioSource soundpopal;

    private void Start()
    {
        //soundpopal = GetComponent<AudioSource>();
    }
    public override void Shot(Vector2 dir, float speed, int layer)
    {
        transform.SetParent(null);
        transform.localScale = Vector3.one;
        transform.right = dir;
        _rb.velocity = dir * speed;
        _collider.isTrigger = true;
        _collider.enabled = true;
        _damageLayer = layer;
        if (speed > 39)
        {
            Destroy(this.gameObject, delay);
            //Debug.Log("Delete arrow");
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (_damageLayer != collision.gameObject.layer) 
            return;
        else if (collision.gameObject.tag == "Object")
        {
            //Debug.Log("enter");
            Destroy(this.gameObject);
        }

        _rb.velocity = Vector2.zero;
        _collider.enabled = false;
        transform.SetParent(collision.transform);
        if(collision.TryGetComponent(out ITakeDamage damage))
        {
            soundpopal.Play();
            damage.Push(transform.position);
            ModifyHealth(damage);
        }
    }
}
