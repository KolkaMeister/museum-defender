namespace Dialogs.States
{
    public class WaitDialogState : DialogState, IEnterState
    {
        public WaitDialogState(DialogMachine machine, DialogBox box) : base(machine, box)
        {
        }

        public void Enter()
        {
        }
    }
}