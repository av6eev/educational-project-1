using Utilities;
using World.Systems.Crop;
using World.Systems.Tree;
using World.WorldElement;

namespace World.Crop
{
    public class CropController : IController
    {
        private readonly CropBlock _model;
        private readonly WorldElementView _view;

        public CropController(CropBlock model, WorldElementView view)
        {
            _model = model;
            _view = view;
        }
        public void Activate()
        {
            _model.IsCrop = true;
            _view.ChangeObject(ContentHelper.Crops[_model.CropType]);
        }

        public void Deactivate()
        {
        }
    }
}