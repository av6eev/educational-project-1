using System;
using Utilities;
using World.Cell;

namespace World
{
    public class WorldController : IController
    {
        private readonly WorldModel _worldModel;
        private readonly WorldView _worldView;
        private readonly GameContext _gameContext;
        private ControllerCollection _controllerCollection = new ControllerCollection();

        public WorldController(WorldModel worldModel, WorldView worldView, GameContext gameContext)
        {
            _worldModel = worldModel;
            _worldView = worldView;
            _gameContext = gameContext;
        }

        public void Activate()
        {
            foreach (var cellModel in _worldModel.CellModels)
            {
                if (_worldModel.LocationData.HasGroundPath)
                {
                    var view = _worldView.Create(cellModel.Position);
                    _controllerCollection.Add(new PathWorldElementController(cellModel.PathElementModel, view));
                }
            }
            _controllerCollection.Activate();
        }

        public void Deactivate()
        {
            _controllerCollection.Deactivate();
            _controllerCollection.Clear();
        }
    }
}