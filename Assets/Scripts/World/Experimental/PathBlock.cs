using System.Collections.Generic;
using UnityEngine;
using Utilities;
using World.Experimental.Systems;
using World.Path;
using World.WorldElement;

namespace World.Experimental
{
    public class PathBlock : Block
    {
        private static Dictionary<Direction, int> _anglesStraight = new Dictionary<Direction, int>()
        {
            {Direction.Right, 0},
            {Direction.Left, -180},
            {Direction.Top, -90},
            {Direction.Bottom, 90},
            {Direction.None, 0},
        };

        private static Dictionary<AngleDirection, int> _anglesRotation = new Dictionary<AngleDirection, int>()
        {
            {AngleDirection.LeftToTop, 180},
            {AngleDirection.RightToBottom, 0},
            {AngleDirection.TopToLeft, 0}, //+
            {AngleDirection.BottomToRight, 180} //+
        };

        public PathTypes PathType;
        public Direction Direction;
        public AngleDirection AngleDirection;
        
        public int Id;
        public bool IsPath;

        public PathBlock(int id, Vector3 position) : base(id, position)
        {
            Type = BlockType.Path;
        }

        public PathBlock(Block block, Direction direction) : base(block)
        {
            Direction = direction;
            Type = BlockType.Path;
        }

        public void Rotate(Direction direction, AngleDirection angleDirection)
        {
            Direction = direction;
            AngleDirection = angleDirection;
            PathType = PathTypes.Bend;
            IsPath = true;
        }
        
        public void SetStartPath()
        {
            Direction = Direction.None;
            PathType = PathTypes.Cross;
            IsPath = true;
        }

        public void SetEndPath()
        {
            PathType = PathTypes.End;
            IsPath = true;
        }

        public void SetDefault()
        {
            PathType = PathTypes.Straight;
            IsPath = true;
        }

        public bool TryGetMoveDirection(BlocksWorldModel model, out PathBlock block, Direction direction = Direction.None)
        {
            if (direction != Direction.None)
            {
                Direction = direction;
            }
            
            switch (Direction)
            {
                case Direction.Left:
                    if (model.Blocks.ContainsKey(LeftBlock) && model.Blocks[LeftBlock].Type != BlockType.Path)
                    {
                        block = new PathBlock(model.Blocks[LeftBlock], Direction);
                        model.Blocks[LeftBlock] = block;
                        IsPath = true;
                        return true;
                    }
                    else if (model.Blocks.ContainsKey(LeftBlock) && model.Blocks[LeftBlock].Type == BlockType.Path)
                    {
                        block = (PathBlock) model.Blocks[LeftBlock];
                        IsPath = true;
                        return true;
                    }
                    else 
                    {
                        block = null;
                        return false;
                    }
                case Direction.Right:
                    if (model.Blocks.ContainsKey(RightBlock) && model.Blocks[RightBlock].Type != BlockType.Path)
                    {
                        block = new PathBlock(model.Blocks[RightBlock], Direction);
                        model.Blocks[RightBlock] = block;
                        IsPath = true;
                        return true;
                    }
                    else if (model.Blocks.ContainsKey(RightBlock) && model.Blocks[RightBlock].Type == BlockType.Path)
                    {
                        block = (PathBlock) model.Blocks[RightBlock];
                        IsPath = true;
                        return true;
                    }
                    else
                    {
                        block = null;
                        return false;
                    }
                case Direction.Top:
                    if (model.Blocks.ContainsKey(TopBlock) && model.Blocks[TopBlock].Type != BlockType.Path)
                    {
                        block = new PathBlock(model.Blocks[TopBlock], Direction);
                        model.Blocks[TopBlock] = block;
                        IsPath = true;
                        return true;
                    }
                    else if (model.Blocks.ContainsKey(TopBlock) && model.Blocks[TopBlock].Type == BlockType.Path)
                    {
                        block = (PathBlock) model.Blocks[TopBlock];
                        IsPath = true;
                        return true;
                    }
                    else
                    {
                        block = null;
                        return false;
                    }
                case Direction.Bottom:
                    if (model.Blocks.ContainsKey(BottomBlock) && model.Blocks[BottomBlock].Type != BlockType.Path)
                    {
                        block = new PathBlock(model.Blocks[BottomBlock], Direction);
                        model.Blocks[BottomBlock] = block;
                        IsPath = true;
                        return true;
                    }
                    else if (model.Blocks.ContainsKey(BottomBlock) && model.Blocks[BottomBlock].Type == BlockType.Path)
                    {
                        block = (PathBlock) model.Blocks[BottomBlock];
                        IsPath = true;
                        return true;
                    }
                    else
                    {
                        block = null;
                        return false;
                    }
            }

            block = null;
            return false;
        }

        public int GetAngle()
        {
            switch (PathType)
            {
                case PathTypes.Straight:
                    return _anglesStraight[Direction];
                    break;
                case PathTypes.Bend:
                    return _anglesRotation[AngleDirection];
                    break;
                case PathTypes.End:
                    return _anglesStraight[Direction];
                    break;
            }

            return 0;
        }
    }
}