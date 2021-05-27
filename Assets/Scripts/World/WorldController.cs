using System;
using Utilities;
using World.Cell;
using World.Tree;

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
            foreach (var worldElementModel in _worldModel.WorldElementModels)
            {
                var view = _worldView.Create(worldElementModel.Position);

                _controllerCollection.Add(new TreeWorldElementController(worldElementModel.TreeElementModel, view));
                
                if (_worldModel.LocationData.HasGroundPath)
                {
                    _controllerCollection.Add(new PathWorldElementController(worldElementModel.PathElementModel, view));
                    view.WorldElement.SetId(worldElementModel.Id);
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