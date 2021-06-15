using System;
using System.Collections.Generic;
using Utilities;
using World.Block;
using World.Systems.Utilities;
using World.Tree;

namespace World.Systems.Tree
{
    public class GenerateTreeSystem : ISystem
    {
        private readonly List<TreeBlock> _active = new List<TreeBlock>();
        private readonly List<TreeBlock> _remove = new List<TreeBlock>();
        private readonly List<TreeBlock> _add = new List<TreeBlock>();
        
        private BlockWorldModel _model;
        private readonly GameContext _context;
        private readonly Action _endGeneration;

        public GenerateTreeSystem(BlockWorldModel model, GameContext context, Action endGeneration)
        {
            _model = model;
            _context = context;
            _endGeneration = endGeneration;
        }

        public void Update()
        {
            foreach (var treeBlock in _remove)
            {
                _active.Remove(treeBlock);
            }

            _remove.Clear();
            
            foreach (var treeBlock in _add)
            {
                _active.Add(treeBlock);
            }

            _add.Clear();

            if (_active.Count == 0)
            {
                _endGeneration?.Invoke();
            }

            foreach (var treeBlock in _active)
            {
                if (!treeBlock.IsTree || !treeBlock.IsPath || !treeBlock.IsBorder || !treeBlock.IsCrop)
                {
                    treeBlock.Set(treeBlock.TreeType, treeBlock.Size);
                    
                    _add.Add(treeBlock);
                }
                
                _remove.Add(treeBlock);
            }
        }

        public void Add(TreeBlock block)
        {
            _add.Add(block);
        }
    }
}