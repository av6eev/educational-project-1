using System;
using System.Collections.Generic;
using Utilities;
using World.Block;
using World.River;
using World.Systems.Path;
using World.Systems.Utilities;
using Random = UnityEngine.Random;

namespace World.Systems.River
{
    public class GenerateRiverSystem : ISystem
    {
        private readonly List<RiverBlock> _active = new List<RiverBlock>();
        private readonly List<RiverBlock> _remove = new List<RiverBlock>();
        private readonly List<RiverBlock> _add = new List<RiverBlock>();

        private BlockWorldModel _model;
        private readonly GameContext _context;
        private readonly Action _endGeneration;

        public GenerateRiverSystem(BlockWorldModel model, GameContext context, Action endGeneration)
        {
            _model = model;
            _context = context;
            _endGeneration = endGeneration;
        }

        public void Update()
        {
            foreach (var riverBlock in _remove)
            {
                _active.Remove(riverBlock);
            }

            _remove.Clear();
            
            foreach (var riverBlock in _add)
            {
                _active.Add(riverBlock);
            }

            _add.Clear();

            if (_active.Count == 0)
            {
                _endGeneration?.Invoke();
            }

            foreach (var riverBlock in _active)
            {
                if (riverBlock.TryGetMoveDirection(_model, out var block))
                {
                    if (block.IsBorder || block.IsCrop || block.IsTree || block.IsPath)
                    {
                        riverBlock.SetEndPath();
                    }
                    else
                    {
                        if (block.IsRiver)
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
                            if (Random.Range(0, 100) < _context.LocationData.ChanceToGenerateRiver)
                            {
                                if (Random.Range(0, 100) < _context.LocationData.RiverRotationChance)
                                {
                                    if (riverBlock.Direction == Direction.Left)
                                    {
                                        block.Rotate(Direction.Top, AngleDirection.LeftToTop);
                                    }

                                    if (riverBlock.Direction == Direction.Right)
                                    {
                                        block.Rotate(Direction.Bottom, AngleDirection.RightToBottom);
                                    }

                                    if (riverBlock.Direction == Direction.Top)
                                    {
                                        block.Rotate(Direction.Left, AngleDirection.TopToLeft);
                                    }

                                    if (riverBlock.Direction == Direction.Bottom)
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

                _remove.Add(riverBlock);
            }
        }


        public void Add(RiverBlock block)
        {
            _add.Add(block);
        }
    }
}