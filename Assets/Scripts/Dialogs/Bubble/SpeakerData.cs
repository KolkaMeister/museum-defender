namespace Dialogs.Sideline
{
    public struct SpeakerData
    {
        public readonly string Name;
        public readonly BubbleDialogView Speaker;

        public SpeakerData(string name, BubbleDialogView speaker)
        {
            Name = name;
            Speaker = speaker;
        }
    }
}