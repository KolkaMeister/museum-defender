public interface IInteractable
{
    public string Description { get; set; }
    public void Interact(Character obj);
}
