using Utilities;
using World.WorldElement;

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
            _model.IsUsed = true;
            _view.ChangeObject(ContentHelper.Trees[_model.TreeType]);
            _view.Scale = _model.TreeSize;
        }

        public void Deactivate()
        {
        }
    }
}