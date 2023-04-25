using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage 
{
    public PersistantProperty<float> Health { set; get; }
    public void TakeDamage(float value);
    public void HealHealth(float value);
}
