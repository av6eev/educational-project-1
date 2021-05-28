using Utilities;
using World.Experimental;
using World.WorldElement;

namespace World.Path
{
    public class PathWorldElementController : IController
    {
        private readonly PathBlock _block;
        private readonly WorldElementView _view;

        public PathWorldElementController(PathBlock block, WorldElementView view)
        {
            _block = block;
            _view = view;
        }
        
        public void Activate()
        {
            _view.ChangeObject(ContentHelper.Pathes[_block.PathType], _block.GetAngle());
            _view.WorldElement.DisableGround();
        }

        public void Deactivate()
        {
        }
    }
}