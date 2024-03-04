using Dialogs;
using Zenject;

namespace Infrastructure
{
    public class DialogInitializer : IInitializable
    {
        private readonly DialogSystem _dialogSys;
        private readonly DialogController _controller;

        public DialogInitializer(DialogSystem dialogSys, DialogController controller)
        {
            _dialogSys = dialogSys;
            _controller = controller;
        }
        
        public void Initialize()
        {
            _dialogSys.SetController(_controller);
        }
    }
}