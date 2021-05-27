using Utilities;
using World.Cell;

namespace World.Tree
{
    public class TreeWorldElementController : IController
    {
        private readonly TreeWorldElementModel _model;
        private readonly WorldElementView _view;

        public TreeWorldElementController(TreeWorldElementModel model, WorldElementView view)
        {
            _model = model;
            _view = view;
        }
        public void Activate()
        {
            
        }

        public void Deactivate()
        {
            
        }
    }
}