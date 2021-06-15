using System;
using System.Collections.Generic;
using Utilities;
using World.Block;
using World.Crop;
using World.Systems.Path;
using World.Systems.Utilities;

namespace World.Systems.Crop
{
    public class GenerateCropSystem : ISystem
    {
        private readonly List<CropBlock> _active = new List<CropBlock>();
        private readonly List<CropBlock> _remove = new List<CropBlock>();
        private readonly List<CropBlock> _add = new List<CropBlock>();
        
        private BlockWorldModel _model;
        private readonly GameContext _context;
        private readonly Action _endGeneration;

        public GenerateCropSystem(BlockWorldModel model, GameContext context, Action endGeneration)
        {
            _model = model;
            _context = context;
            _endGeneration = endGeneration;
        }
        
        public void Update()
        {
            foreach (var pathBlock in _remove)
            {
                _active.Remove(pathBlock);
            }

            _remove.Clear();
            foreach (var pathBlock in _add)
            {
                _active.Add(pathBlock);
            }

            _add.Clear();

            if (_active.Count == 0)
            {
                _endGeneration?.Invoke();
            }

            foreach (var cropBlock in _active)
            {
                if (!cropBlock.IsTree || !cropBlock.IsPath || !cropBlock.IsBorder || !cropBlock.IsCrop)
                {
                    cropBlock.Set(cropBlock.CropType);
                    
                    _add.Add(cropBlock);
                }
                
                _remove.Add(cropBlock);
            }
        }

        public void Add(CropBlock block)
        {
            _add.Add(block);
        }
    }
}