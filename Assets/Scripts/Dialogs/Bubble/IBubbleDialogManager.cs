namespace Dialogs.Sideline
{
    public interface IBubbleDialogManager
    {
        public void CreateDialog(Character initiator, Character initiated, string template);
        public void Interrupt(Character speaker);
        public bool HasDialog(Character speaker);
        public void RemoveDialog(IBubbleDialogSystem system);
    }
}