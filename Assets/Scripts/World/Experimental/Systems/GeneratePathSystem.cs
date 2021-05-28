using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using World.Path;
using Random = UnityEngine.Random;

namespace World.Experimental.Systems
{
    public class GeneratePathSystem : ISystem
    {
        private readonly List<PathBlock> _active = new List<PathBlock>();
        private readonly List<PathBlock> _remove = new List<PathBlock>();
        private readonly List<PathBlock> _add = new List<PathBlock>();
        private BlocksWorldModel _model;
        private readonly GameContext _context;
        private readonly Action _endGeneration;

        public GeneratePathSystem(BlocksWorldModel model, GameContext context, Action endGeneration)
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
                Debug.Log("asdasd");
                if (Random.Range(0, 100) < _context.LocationData.ChanceToGeneratePath)
                {
                    if (Random.Range(0, 100) < _context.LocationData.RotationChance)
                    {
                        var randomNumber = Random.Range(0, 100);

                        if (pathBlock.Direction == Direction.Left)
                        {
                            if (randomNumber < 50)
                            {
                                pathBlock.Rotate(Direction.Top, Direction.LeftToTop);
                            }
                            else
                            {
                                pathBlock.Rotate(Direction.Bottom, Direction.LeftToBottom);
                            }
                        }

                        if (pathBlock.Direction == Direction.Right)
                        {
                            if (randomNumber < 50)
                            {
                                pathBlock.Rotate(Direction.Top, Direction.RightToTop);
                            }
                            else
                            {
                                pathBlock.Rotate(Direction.Bottom, Direction.RightToBottom);
                            }
                        }

                        if (pathBlock.Direction == Direction.Top)
                        {
                            if (randomNumber < 50)
                            {
                                pathBlock.Rotate(Direction.Left, Direction.TopToLeft);
                            }
                            else
                            {
                                pathBlock.Rotate(Direction.Right, Direction.TopToRight);
                            }
                        }

                        if (pathBlock.Direction == Direction.Bottom)
                        {
                            if (randomNumber < 50)
                            {
                                pathBlock.Rotate(Direction.Left, Direction.BottomToLeft);
                            }
                            else
                            {
                                pathBlock.Rotate(Direction.Right, Direction.BottomToRight);
                            }
                        }
                        
                        if (pathBlock.TryGetMoveDirection(_model, out var block))
                        {
                            _add.Add(block);
                        }
                    }
                    else
                    {
                        if (pathBlock.TryGetMoveDirection(_model, out var block))
                        {
                            _add.Add(block);
                            block.SetDefault();
                        }
                        else
                        {
                            pathBlock.SetEndPath();
                        }
                    }
                }
                else
                {
                    if (pathBlock.TryGetMoveDirection(_model, out var block))
                    {
                        block.SetEndPath();
                    }
                    else
                    {
                        pathBlock.SetEndPath();
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