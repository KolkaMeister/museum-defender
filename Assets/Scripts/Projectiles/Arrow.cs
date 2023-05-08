using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageLayer == collision.gameObject.layer && collision.gameObject.GetComponent<ITakeDamage>() != null)
        {
            ModifyHealth(collision.gameObject.GetComponent<ITakeDamage>());
            _rb.velocity = Vector2.zero;
            transform.SetParent(collision.gameObject.transform);
        }
    }
}
