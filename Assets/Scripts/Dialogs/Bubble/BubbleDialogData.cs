namespace Dialogs.Sideline
{
    public struct BubbleDialogData
    {
        public readonly Character[] Speakers;
        public readonly IBubbleDialogSystem System;

        public BubbleDialogData(Character[] speakers, IBubbleDialogSystem system)
        {
            Speakers = speakers;
            System = system;
        }
    }
}