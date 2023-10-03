public interface ITakeDamage 
{
    public PersistantProperty<float> Health { set; get; }

    public bool IsDead { set; get; }
    public void ChangeHealth(float value);
}
