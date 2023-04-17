using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _modifyValue;
    [SerializeField] private LayerMask damageLayer;
    [SerializeField] private Rigidbody2D _rb;
    public void Awake()
    {
        _rb=GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (damageLayer.value==collision.gameObject.layer|| collision.gameObject.GetComponent<ITakeDamage>()!=null)
        {
            ModifyHealth(collision.gameObject.GetComponent<ITakeDamage>());
        }
    }
    private void ModifyHealth(ITakeDamage obj)
    {
        if (_modifyValue<=0)
            obj.TakeDamage(Mathf.Abs(_modifyValue));
        else
            obj.HealHealth(_modifyValue);
    }
    public void AddForce(Vector3 _dir,float _multi)
    {
        transform.rotation = Quaternion.Euler(_dir);
        Debug.Log(_dir);
        Debug.Log(Mathf.Cos(transform.rotation.z));
        Debug.Log(Mathf.Sin(transform.rotation.z));
        _rb.velocity= new Vector2(Mathf.Cos(transform.rotation.z), Mathf.Sin(transform.rotation.z)) * _multi;
    }
}
