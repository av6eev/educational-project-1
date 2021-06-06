using Utilities;
using World.Systems.River;
using World.WorldElement;

namespace World.River
{
    public class RiverController : IController
    {
        private readonly RiverBlock _model;
        private readonly WorldElementView _view;

        public RiverController(RiverBlock model, WorldElementView view)
        {
            _model = model;
            _view = view;
        }
        public void Activate()
        {
            _model.IsRiver = true;
            _view.ChangeObject(ContentHelper.Rivers[_model.RiverType], _model.GetAngle());
            _view.WorldElement.DisableGround();
        }

        public void Deactivate()
        {
        }
    }
}