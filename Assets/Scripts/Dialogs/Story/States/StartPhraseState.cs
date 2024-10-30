namespace Dialogs.States
{
    public class StartPhraseState : DialogState, IEnterState, IUpdateState
    {
        public StartPhraseState(DialogMachine machine, DialogBox box) : base(machine, box)
        {
        }

        public void Enter()
        {
            _box.Clear();
        }

        public void Update()
        {
            _machine.ChangeState<TypePhraseState>();
        }
    }
}