using Utilities;
using World.Systems.Path;
using World.WorldElement;

namespace World.Path
{
    public class PathController : IController
    {
        private readonly PathBlock _model;
        private readonly WorldElementView _view;

        public PathController(PathBlock model, WorldElementView view)
        {
            _model = model;
            _view = view;
        }
        
        public void Activate()
        {
            _view.ChangeObject(ContentHelper.Pathes[_model.PathType], _model.GetAngle());
            _view.WorldElement.DisableGround();
        }

        public void Deactivate()
        {
        }
    }
}