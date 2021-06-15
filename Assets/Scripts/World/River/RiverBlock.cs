using System.Collections.Generic;
using UnityEngine;
using Utilities;
using World.Block;

namespace World.River
{
    public class RiverBlock : BaseBlock
    {
        private static Dictionary<Direction, int> _anglesStraight = new Dictionary<Direction, int>()
        {
            {Direction.Right, 0},
            {Direction.Left, -180},
            {Direction.Top, -90},
            {Direction.Bottom, 90},
            {Direction.None, 0}
        };

        private static Dictionary<AngleDirection, int> _anglesRotation = new Dictionary<AngleDirection, int>()
        {
            {AngleDirection.LeftToTop, 180},
            {AngleDirection.RightToBottom, 0},
            {AngleDirection.TopToLeft, 0}, 
            {AngleDirection.BottomToRight, 180} 
        };

        public RiverTypes RiverType;
        public Direction Direction;
        public AngleDirection AngleDirection;
        
        public RiverBlock(int id, Vector3 position) : base(id, position)
        {
            Type = BlockType.River;
        }

        public RiverBlock(BaseBlock block, Direction direction) : base(block)
        {
            Direction = direction;
            Type = BlockType.River;
        }
        
        public void Rotate(Direction direction, AngleDirection angleDirection)
        {
            Direction = direction;
            AngleDirection = angleDirection;
            RiverType = RiverTypes.Bend;
            IsRiver = true;
        }
        
        public void SetStartPath()
        {
            Direction = Direction.None;
            RiverType = RiverTypes.Cross;
            IsRiver = true;
        }

        public void SetEndPath()
        {
            RiverType = RiverTypes.End;
            IsRiver = true;
        }

        public void SetDefault()
        {
            var random = Random.Range(0, 100);
            
            if (random < 50)
            {
                RiverType = RiverTypes.Straight;
            }
            else
            {
                RiverType = RiverTypes.StraightWithRocks;
            }
            
            IsRiver = true;
        }

        public bool TryGetMoveDirection(BlockWorldModel model, out RiverBlock block, Direction direction = Direction.None)
        {
            if (direction != Direction.None)
            {
                Direction = direction;
            }
            
            switch (Direction)
            {
                case Direction.Left:
                    if (model.Blocks.ContainsKey(LeftBlock) && model.Blocks[LeftBlock].Type != BlockType.River)
                    {
                        block = new RiverBlock(model.Blocks[LeftBlock], Direction);
                        model.Blocks[LeftBlock] = block;
                        IsRiver = true;
                        return true;
                    }
                    else if (model.Blocks.ContainsKey(LeftBlock) && model.Blocks[LeftBlock].Type == BlockType.River)
                    {
                        block = (RiverBlock) model.Blocks[LeftBlock];
                        IsRiver = true;
                        return true;
                    }
                    else 
                    {
                        block = null;
                        return false;
                    }
                case Direction.Right:
                    if (model.Blocks.ContainsKey(RightBlock) && model.Blocks[RightBlock].Type != BlockType.River)
                    {
                        block = new RiverBlock(model.Blocks[RightBlock], Direction);
                        model.Blocks[RightBlock] = block;
                        IsRiver = true;
                        return true;
                    }
                    else if (model.Blocks.ContainsKey(RightBlock) && model.Blocks[RightBlock].Type == BlockType.River)
                    {
                        block = (RiverBlock) model.Blocks[RightBlock];
                        IsRiver = true;
                        return true;
                    }
                    else
                    {
                        block = null;
                        return false;
                    }
                case Direction.Top:
                    if (model.Blocks.ContainsKey(TopBlock) && model.Blocks[TopBlock].Type != BlockType.River)
                    {
                        block = new RiverBlock(model.Blocks[TopBlock], Direction);
                        model.Blocks[TopBlock] = block;
                        IsRiver = true;
                        return true;
                    }
                    else if (model.Blocks.ContainsKey(TopBlock) && model.Blocks[TopBlock].Type == BlockType.River)
                    {
                        block = (RiverBlock) model.Blocks[TopBlock];
                        IsRiver = true;
                        return true;
                    }
                    else
                    {
                        block = null;
                        return false;
                    }
                case Direction.Bottom:
                    if (model.Blocks.ContainsKey(BottomBlock) && model.Blocks[BottomBlock].Type != BlockType.River)
                    {
                        block = new RiverBlock(model.Blocks[BottomBlock], Direction);
                        model.Blocks[BottomBlock] = block;
                        IsRiver = true;
                        return true;
                    }
                    else if (model.Blocks.ContainsKey(BottomBlock) && model.Blocks[BottomBlock].Type == BlockType.River)
                    {
                        block = (RiverBlock) model.Blocks[BottomBlock];
                        IsRiver = true;
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
            switch (RiverType)
            {
                case RiverTypes.Straight:
                    return _anglesStraight[Direction];
                    break;
                case RiverTypes.StraightWithRocks:
                    return _anglesStraight[Direction];
                    break;
                case RiverTypes.Bend:
                    return _anglesRotation[AngleDirection];
                    break;
                case RiverTypes.End:
                    return _anglesStraight[Direction];
                    break;
            }

            return 0;
        }
    }
}