using System;
using System.Collections.Generic;
using Utilities;
using World.Block;
using World.Systems.Utilities;
using Random = UnityEngine.Random;

namespace World.Systems.Path
{
    public class GeneratePathSystem : ISystem
    {
        private readonly List<PathBlock> _active = new List<PathBlock>();
        private readonly List<PathBlock> _remove = new List<PathBlock>();
        private readonly List<PathBlock> _add = new List<PathBlock>();
        
        private BlockWorldModel _model;
        private readonly GameContext _context;
        private readonly Action _endGeneration;

        public GeneratePathSystem(BlockWorldModel model, GameContext context, Action endGeneration)
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

            foreach (var pathBlock in _active)
            {
                if (pathBlock.TryGetMoveDirection(_model, out var block))
                {
                    if (block.IsBorder || block.IsCrop || block.IsTree)
                    {
                        block.SetEndPath();
                    }
                    else if (block.IsRiver)
                    {
                        block.SetBridge();
                    }
                    else
                    {
                        if (block.IsPath)
                        {
                            block.SetStartPath();

                            if (_model.Blocks.ContainsKey(block.LeftBlock) && _model.Blocks[block.LeftBlock].Type != BlockType.Ground)
                            {
                                if (block.TryGetMoveDirection(_model, out var leftBlock, Direction.Left))
                                {
                                    leftBlock.SetDefault();
                                    _add.Add(leftBlock);
                                }
                            }

                            if (_model.Blocks.ContainsKey(block.RightBlock) && _model.Blocks[block.RightBlock].Type != BlockType.Ground)
                            {
                                if (block.TryGetMoveDirection(_model, out var rightBlock, Direction.Right))
                                {
                                    rightBlock.SetDefault();
                                    _add.Add(rightBlock);
                                }
                            }

                            if (_model.Blocks.ContainsKey(block.TopBlock) && _model.Blocks[block.TopBlock].Type != BlockType.Ground)
                            {
                                if (block.TryGetMoveDirection(_model, out var topBlock, Direction.Top))
                                {
                                    topBlock.SetDefault();
                                    _add.Add(topBlock);
                                }
                            }

                            if (_model.Blocks.ContainsKey(block.BottomBlock) && _model.Blocks[block.BottomBlock].Type != BlockType.Ground)
                            {
                                if (block.TryGetMoveDirection(_model, out var bottomBlock, Direction.Bottom))
                                {
                                    bottomBlock.SetDefault();
                                    _add.Add(bottomBlock);
                                }
                            }
                        }
                        else
                        {
                            if (Random.Range(0, 100) < _context.LocationData.ChanceToGeneratePath)
                            {
                                if (Random.Range(0, 100) < _context.LocationData.PathRotationChance)
                                {
                                    if (pathBlock.Direction == Direction.Left)
                                    {
                                        block.Rotate(Direction.Top, AngleDirection.LeftToTop);
                                    }

                                    if (pathBlock.Direction == Direction.Right)
                                    {
                                        block.Rotate(Direction.Bottom, AngleDirection.RightToBottom);
                                    }

                                    if (pathBlock.Direction == Direction.Top)
                                    {
                                        block.Rotate(Direction.Left, AngleDirection.TopToLeft);
                                    }

                                    if (pathBlock.Direction == Direction.Bottom)
                                    {
                                        block.Rotate(Direction.Right, AngleDirection.BottomToRight);
                                    }
                                }
                                else
                                {
                                    block.SetDefault();
                                }

                                _add.Add(block);
                            }
                            else
                            {
                                block.SetEndPath();
                            }
                        }
                    }
                }

                _remove.Add(pathBlock);
            }
        }

        public void Add(PathBlock block)
        {
            _add.Add(block);
        }
    }
}