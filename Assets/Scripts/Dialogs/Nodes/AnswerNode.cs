namespace Dialogs.Nodes
{
    public class AnswerNode : DialogNode
    {
        public string Line { get; private set; }
        
        public AnswerNode(string text, string line, string tag = "") : base(text, "", tag, PhraseType.Answer)
        {
            Line = line;
        }
    }
}