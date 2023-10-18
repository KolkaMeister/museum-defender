using Dialogs;
using Zenject;

namespace Infrastructure
{
    public class Initialization : IInitializable
    {
        private readonly DialogSystem _dialogSys;
        private readonly DialogController _controller;

        public Initialization(DialogSystem dialogSys, DialogController controller)
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