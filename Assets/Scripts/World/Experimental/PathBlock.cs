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
            {Utilities.Direction.Right, 0},
            {Utilities.Direction.Left, -180},
            {Utilities.Direction.Top, -90},
            {Utilities.Direction.Bottom, 90},
            {Utilities.Direction.None, 0},
            // {Utilities.Direction.LeftToTop, -90},
            // {Utilities.Direction.LeftToBottom, 90},
            // {Utilities.Direction.RightToTop, -90},
            // {Utilities.Direction.RightToBottom, 90},
            // {Utilities.Direction.TopToLeft, -180},
            // {Utilities.Direction.TopToRight, 0},
            // {Utilities.Direction.BottomToLeft, -180},
            // {Utilities.Direction.BottomToRight, 0}
        };

        private static Dictionary<Direction, int> _anglesRotation = new Dictionary<Direction, int>()
        {
            {Utilities.Direction.LeftToTop, 180},
            {Utilities.Direction.LeftToBottom, -90},
            {Utilities.Direction.RightToTop, 90},
            {Utilities.Direction.RightToBottom, 0},
            {Utilities.Direction.TopToLeft, 0},
            {Utilities.Direction.TopToRight, -90},
            {Utilities.Direction.BottomToLeft, 90},
            {Utilities.Direction.BottomToRight, 180},
            {Utilities.Direction.None, 0}
        };

        public PathTypes PathType;
        public Direction Direction;
        public Direction AngleDirection;
        
        public int Id;

        public PathBlock(int id, Vector3 position) : base(id, position)
        {
            Type = BlockType.Path;
        }

        public PathBlock(Block block, Direction direction) : base(block)
        {
            Direction = direction;
            Type = BlockType.Path;
        }

        public void Rotate(Direction direction, Direction angleDirection)
        {
            Direction = direction;
            AngleDirection = angleDirection;
            PathType = PathTypes.Bend;
        }
        
        public void SetStartPath()
        {
            Direction = Direction.None;
            PathType = PathTypes.Cross;
        }

        public void SetEndPath()
        {
            PathType = PathTypes.End;
        }

        public void SetDefault()
        {
            PathType = PathTypes.Straight;
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
                    if (model.Blocks.ContainsKey(LeftBlock))
                    {
                        block = new PathBlock(model.Blocks[LeftBlock], Direction);
                        model.Blocks[LeftBlock] = block;
                        return true;
                    }
                    else
                    {
                        block = null;
                        return false;
                    }
                case Direction.Right:
                    if (model.Blocks.ContainsKey(RightBlock))
                    {
                        block = new PathBlock(model.Blocks[RightBlock], Direction);
                        model.Blocks[RightBlock] = block;
                        return true;
                    }
                    else
                    {
                        block = null;
                        return false;
                    }
                case Direction.Top:
                    if (model.Blocks.ContainsKey(TopBlock))
                    {
                        block = new PathBlock(model.Blocks[TopBlock], Direction);
                        model.Blocks[TopBlock] = block;
                        return true;
                    }
                    else
                    {
                        block = null;
                        return false;
                    }
                case Direction.Bottom:
                    if (model.Blocks.ContainsKey(BottomBlock))
                    {
                        block = new PathBlock(model.Blocks[BottomBlock], Direction);
                        model.Blocks[BottomBlock] = block;
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