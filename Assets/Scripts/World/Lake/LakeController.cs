using Utilities;
using World.WorldElement;

namespace World.Lake
{
    public class LakeController : IController
    {
        private readonly LakeBlock _model;
        private readonly WorldElementView _view;

        public LakeController(LakeBlock model, WorldElementView view)
        {
            _model = model;
            _view = view;
        }
        
        public void Activate()
        {
            _model.IsLake = true;
            _view.ChangeObject(ContentHelper.Lakes[_model.LakeType], _model.GetAngle());
            _view.WorldElement.DisableGround();
        }

        public void Deactivate()
        {
        }
    }
}