using Dialogs.Nodes;

namespace Dialogs.Sideline
{
    public interface IBubbleDialogController
    {
        public BubbleDialogView Current { get; }
        public DialogNode CurrentNode { get; }

        public void Start(SpeakerData initiator, SpeakerData initiated);
        public DialogNode ReadNextNode();
        public void Stop();
    }
}