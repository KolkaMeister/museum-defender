using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private float speed;
    private float delay = 0.2f;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageLayer == collision.gameObject.layer && collision.gameObject.GetComponent<ITakeDamage>() != null)
        {
            ModifyHealth(collision.gameObject.GetComponent<ITakeDamage>());
            _rb.velocity = Vector2.zero;
            transform.SetParent(collision.gameObject.transform);
        }
        else if (collision.gameObject.tag == "Object") //При соприкосновении с объектом с тегом объект удаляется
        {
            //Debug.Log("enter");
            DestroyOnHit();
        }
    }
    private void Update()
    {
        speed = GetComponent<Rigidbody2D>().velocity.magnitude; //При достижения стрелы определённой скорости через время она удалится
        if (speed > 39)
        {
            Destroy(this.gameObject, delay);
            //Debug.Log("Delete arrow");
        }
    }

}
