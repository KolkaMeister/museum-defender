public interface IInteractable
{
    public InteractionType Id { get; set; }
    
    public string Description { get; set; }
    public void Interact(Character obj);
}
