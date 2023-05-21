using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    [SerializeField] bool _destroyOnHit;
    [SerializeField] protected float _modifyValue;
    [SerializeField] public int damageLayer;
    [SerializeField] protected Rigidbody2D _rb;
    public void Awake()
    {
        _rb=GetComponent<Rigidbody2D>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageLayer == collision.gameObject.layer && collision.gameObject.GetComponent<ITakeDamage>()!=null)
        {
            ModifyHealth(collision.gameObject.GetComponent<ITakeDamage>());
            if (_destroyOnHit)
                DestroyOnHit();
        }
    }
    protected virtual void ModifyHealth(ITakeDamage obj)
    {
        if (_modifyValue<=0)
            obj.TakeDamage(Mathf.Abs(_modifyValue));
        else
            obj.HealHealth(_modifyValue);
    }
    public virtual void AddForce(Vector3 _dir,float _multi)
    {
        transform.rotation = Quaternion.Euler(0,0,MathF.Atan2(_dir.y,_dir.x)*360/(2*MathF.PI));
        //Debug.Log(_dir);
        //_rb.velocity= new Vector2(Mathf.Cos(transform.rotation.z), Mathf.Sin(transform.rotation.z)) * _multi;
        _rb.velocity = transform.right * _multi;
    }
    private void DestroyOnHit()
    {
        Destroy(gameObject);
    }
}
