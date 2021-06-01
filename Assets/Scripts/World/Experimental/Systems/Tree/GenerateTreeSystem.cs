using System;
using System.Collections.Generic;
using Utilities;

namespace World.Experimental.Systems.Tree
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

            foreach (var treeBlock in _active)
            {
                if (!treeBlock.IsTree || !treeBlock.IsBorder || !treeBlock.IsPath)
                {
                    treeBlock.Set(treeBlock.TreeType, treeBlock.TreeSize);
                }
            }
        }

        public void Add(TreeBlock block)
        {
            _add.Add(block);
        }
    }
}