using System;
using UnityEngine;
using Utilities;
using World.Block;
using World.Crop;
using World.Lake;
using World.Path;
using World.River;
using World.Systems.Crop;
using World.Systems.Lake;
using World.Systems.Path;
using World.Systems.River;
using World.Systems.Tree;
using World.Tree;

namespace World
{
    public class WorldController : IController
    {
        private readonly BlockWorldModel _worldModel;
        private readonly WorldView _worldView;
        private readonly GameContext _context;
        
        private ControllerCollection _controllerCollection = new ControllerCollection();

        public WorldController(BlockWorldModel worldModel, WorldView worldView, GameContext context)
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
                
                if (block.Value.IsBorder)
                {
                    worldElementView.WorldElement.MeshRenderer.material.color = Color.red;
                }

                if (_context.LocationData.HasPath && block.Value.Type == BlockType.Path)
                {
                    _controllerCollection.Add(new PathController((PathBlock) block.Value, worldElementView));
                }
                
                if (!block.Value.IsBorder && block.Value.Type == BlockType.River)
                {
                    _controllerCollection.Add(new RiverController((RiverBlock) block.Value, worldElementView));
                }

                if (!block.Value.IsBorder && block.Value.Type == BlockType.Tree)
                {
                    _controllerCollection.Add(new TreeController((TreeBlock) block.Value, worldElementView));
                }

                if (!block.Value.IsBorder && block.Value.Type == BlockType.Crop)
                {
                    _controllerCollection.Add(new CropController((CropBlock) block.Value, worldElementView));
                }

                if (!block.Value.IsBorder && block.Value.Type == BlockType.Lake)
                {
                    _controllerCollection.Add(new LakeController((LakeBlock) block.Value, worldElementView));
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