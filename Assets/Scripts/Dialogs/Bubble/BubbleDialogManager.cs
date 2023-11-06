using System.Collections.Generic;
using System.Linq;
using Infrastructure;

namespace Dialogs.Sideline
{
    public class BubbleDialogManager : IBubbleDialogManager
    {
        private readonly IDialogPicker _picker;
        private readonly ICoroutineRunner _runner;
        private readonly List<BubbleDialogData> _dialogs = new List<BubbleDialogData>();

        public BubbleDialogManager(IDialogPicker picker, ICoroutineRunner runner)
        {
            _picker = picker;
            _runner = runner;
        }

        public void CreateDialog(Character initiator, Character initiated, string template)
        {
            Interrupt(initiator);
            Interrupt(initiated);
            
            var system = new BubbleDialogSystem(this, _runner);

            _dialogs.Add(new BubbleDialogData(new[] { initiator, initiated }, system));

            system.Start(initiator, initiated, _picker.GetRandomDialog(template));
        }

        public void Interrupt(Character speaker)
        {
            if (HasDialog(speaker))
            {
                BubbleDialogData data = _dialogs.Single(x => x.Speakers.Contains(speaker));
                data.System.Finish();
                _dialogs.Remove(data);
            }
        }

        public void RemoveDialog(IBubbleDialogSystem system)
        {
            BubbleDialogData data = _dialogs.Single(x => x.System == system);
            _dialogs.Remove(data);
        }

        public bool HasDialog(Character speaker) => _dialogs.Any(x => x.Speakers.Contains(speaker));
    }
}