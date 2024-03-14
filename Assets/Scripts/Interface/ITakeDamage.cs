using UnityEngine;

public interface ITakeDamage 
{
    public PersistantProperty<float> Health { set; get; }

    public bool IsDead { set; get; }
    public void AddHealth(float value);

    public void Push(Vector3 origin);
}
