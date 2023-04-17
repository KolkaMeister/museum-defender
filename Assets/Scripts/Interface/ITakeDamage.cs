using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage 
{
    public delegate void healthChange(float _old, float _current);
    public event healthChange OnChange;
    public float Health { set; get; }
    public void TakeDamage(float value);
    public void HealHealth(float value);
}
