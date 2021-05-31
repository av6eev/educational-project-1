using System;
using UnityEngine;
using Utilities;
using World.Experimental;
using World.Experimental.Systems;
using World.Path;
using World.Tree;

namespace World
{
    public class WorldController : IController
    {
        private readonly BlocksWorldModel _worldModel;
        private readonly WorldView _worldView;
        private readonly GameContext _context;
        private ControllerCollection _controllerCollection = new ControllerCollection();

        public WorldController(BlocksWorldModel worldModel, WorldView worldView, GameContext context)
        {
            _worldModel = worldModel;
            _worldView = worldView;
            _context = context;
        }

        public void Activate()
        {
            foreach (var block in _worldModel.Blocks)
            {
                var worldElementView = _worldView.CreateGround(block.Key);

                if (_context.LocationData.HasGroundPath && block.Value.Type == BlockType.Path)
                {
                    _controllerCollection.Add(new PathWorldElementController((PathBlock) block.Value, worldElementView));
                }

                if (block.Value.IsBorder)
                {
                    worldElementView.WorldElement.MeshRenderer.material.color = Color.red;
                }
                // if (block.IsTree)
                // {
                //     _controllerCollection.Add(new TreeWorldElementController(block.TreeElementModel, worldElementView));
                // }
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