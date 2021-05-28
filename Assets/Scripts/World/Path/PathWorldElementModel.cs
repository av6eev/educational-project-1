using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using World.WorldElement;

namespace World.Path
{
    public class PathWorldElementModel
    {
        public event Action<PathTypes, int> Update;

        private static Dictionary<Direction, int> _anglesStraight = new Dictionary<Direction, int>()
        {
            {Utilities.Direction.Right, 0},
            {Utilities.Direction.Left, -180},
            {Utilities.Direction.Top, -90},
            {Utilities.Direction.Bottom, 90},
            {Utilities.Direction.None, 0},
            {Utilities.Direction.LeftToTop, -90},
            {Utilities.Direction.LeftToBottom, 90},
            {Utilities.Direction.RightToTop, -90},
            {Utilities.Direction.RightToBottom, 90},
            {Utilities.Direction.TopToLeft, -180},
            {Utilities.Direction.TopToRight, 0},
            {Utilities.Direction.BottomToLeft, -180},
            {Utilities.Direction.BottomToRight, 0}
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
            {Utilities.Direction.BottomToRight, 180}
        };

        public LocationData Data;
        public PathTypes PathTypes;
        public Direction Direction;
        public int Id;

        public WorldElementModel LeftNearbyElement;
        public WorldElementModel RightNearbyElement;
        public WorldElementModel TopNearbyElement;
        public WorldElementModel BottomNearbyElement;

        public bool IsUsed;
        public bool ExistLeftNearbyElement = false;
        public bool ExistRightNearbyElement = false;
        public bool ExistTopNearbyElement = false;
        public bool ExistBottomNearbyElement = false;

        public void SearchNearby(WorldModel model, int id, LocationData data)
        {
            Id = id;
            Data = data;

            var raw = id / Data.X;
            var idInLine = id - raw * data.X;

            if (id % (data.X - 1) != 0 && id + 1 != data.X - 1)
            {
                RightNearbyElement = model.WorldElementModels[id + 1];
                ExistRightNearbyElement = true;
            }

            if (id % (data.X - 1) != 1 && id > 0)
            {
                LeftNearbyElement = model.WorldElementModels[id - 1];
                ExistLeftNearbyElement = true;
            }

            if (raw != 0)
            {
                TopNearbyElement = model.WorldElementModels[(raw - 1) * data.X + idInLine];
                ExistTopNearbyElement = true;
            }

            if (raw != data.Z - 1)
            {
                var dataGroundLength = (raw + 1) * data.X + idInLine;
                BottomNearbyElement = model.WorldElementModels[dataGroundLength];
                ExistBottomNearbyElement = true;
            }
        }

        #region Movement

        public void MoveLeft(PathTypes pathType = PathTypes.Straight)
        {
            Direction = Direction.Left;
            if (ExistLeftNearbyElement)
            {
                LeftNearbyElement.TransformObject(ObjectTypes.Path);
                LeftNearbyElement.PathElementModel.Direction = Direction.Left;
            }
            else
            {
                Update?.Invoke(PathTypes.End, 0);
            }
        }

        public void MoveRight(PathTypes pathType = PathTypes.Straight)
        {
            Direction = Direction.Right;
            if (ExistRightNearbyElement)
            {
                RightNearbyElement.TransformObject(ObjectTypes.Path);
                RightNearbyElement.PathElementModel.Direction = Direction.Right;
            }
            else
            {
                Update?.Invoke(PathTypes.End, 0);
            }
        }

        public void MoveTop(PathTypes pathType = PathTypes.Straight)
        {
            Direction = Direction.Top;
            if (ExistTopNearbyElement)
            {
                TopNearbyElement.TransformObject(ObjectTypes.Path);
                TopNearbyElement.PathElementModel.Direction = Direction.Top;
            }
            else
            {
                Update?.Invoke(PathTypes.End, 0);
            }
        }

        public void MoveBottom(PathTypes pathType = PathTypes.Straight)
        {
            Direction = Direction.Bottom;
            if (ExistBottomNearbyElement)
            {
                BottomNearbyElement.TransformObject(ObjectTypes.Path);
                BottomNearbyElement.PathElementModel.Direction = Direction.Bottom;
            }
            else
            {
                Update?.Invoke(PathTypes.End, 0);
            }
        }

        #endregion

        #region Rotation

        public void RotateFromLeftToTop()
        {
            Direction = Direction.Top;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[Direction.LeftToTop]);
        }

        public void RotateFromLeftToBottom()
        {
            Direction = Direction.Bottom;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[Direction.LeftToBottom]);
        }

        public void RotateFromRightToTop()
        {
            Direction = Direction.Top;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[Direction.RightToTop]);
        }

        public void RotateFromRightToBottom()
        {
            Direction = Direction.Bottom;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[Direction.RightToBottom]);
        }

        public void RotateFromTopToLeft()
        {
            Direction = Direction.Left;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[Direction.TopToLeft]);
        }

        public void RotateFromTopToRight()
        {
            Direction = Direction.Right;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[Direction.TopToRight]);
        }

        public void RotateFromBottomToLeft()
        {
            Direction = Direction.Left;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[Direction.BottomToLeft]);
        }

        public void RotateFromBottomToRight()
        {
            Direction = Direction.Right;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[Direction.BottomToRight]);
        }

        #endregion

        public void SetStartPath()
        {
            Update?.Invoke(PathTypes.Cross, 0);
            Direction = Direction.None;
        }

        public void SetEndPath()
        {
            Update?.Invoke(PathTypes.End, _anglesStraight[Direction]);
            Direction = Direction.None;
        }

        public void SetDefault()
        {
            Debug.Log(Id);
            Update?.Invoke(PathTypes.Straight, _anglesStraight[Direction]);
        }

        public PathWorldElementModel GetDirectionModel()
        {
            switch (Direction)
            {
                case Direction.Left:
                    return LeftNearbyElement?.PathElementModel;
                case Direction.Right:
                    return RightNearbyElement?.PathElementModel;
                case Direction.Top:
                    return TopNearbyElement?.PathElementModel;
                case Direction.Bottom:
                    return BottomNearbyElement?.PathElementModel;
            }

            return null;
        }
    }
}