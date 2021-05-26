using UnityEngine;
using Utilities;
using World.Cell;

namespace World
{
    public class PathWorldElementController : IController
    {
        private readonly PathWorldElementModel _model;
        private readonly WorldElementView _view;

        public PathWorldElementController(PathWorldElementModel model, WorldElementView view)
        {
            _model = model;
            _view = view;
        }
        
        public void Activate()
        {
            _model.Update += OnUpdate;
        }

        public void Deactivate()
        {
            _model.Update -= OnUpdate;
        }

        private void OnUpdate(PathTypes obj, int angle)
        {
            _model.IsUsed = true;
            _view.ChangeObject(obj, angle);
        }
    }
}