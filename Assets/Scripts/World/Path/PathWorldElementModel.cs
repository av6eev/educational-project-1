using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using World.Cell;

namespace World
{
    public class PathWorldElementModel
    {
        public event Action<PathTypes, int> Update;

        private static Dictionary<PathDirection, int> _anglesStraight = new Dictionary<PathDirection, int>()
        {
            {PathDirection.Right, 0},
            {PathDirection.Left, -180},
            {PathDirection.Top, -90},
            {PathDirection.Bottom, 90},
            {PathDirection.None, 0},
            {PathDirection.LeftToTop, -90},
            {PathDirection.LeftToBottom, 90},
            {PathDirection.RightToTop, -90},
            {PathDirection.RightToBottom, 90},
            {PathDirection.TopToLeft, -180},
            {PathDirection.TopToRight, 0},
            {PathDirection.BottomToLeft, -180},
            {PathDirection.BottomToRight, 0}
        };

        private static Dictionary<PathDirection, int> _anglesRotation = new Dictionary<PathDirection, int>()
        {
            {PathDirection.LeftToTop, 180},
            {PathDirection.LeftToBottom, -90},
            {PathDirection.RightToTop, 90},
            {PathDirection.RightToBottom, 0}, //0
            {PathDirection.TopToLeft, 0}, 
            {PathDirection.TopToRight, -90},
            {PathDirection.BottomToLeft, 90}, //-180
            {PathDirection.BottomToRight, 180}
        };

        public LocationData Data;
        public PathTypes PathTypes;
        public PathDirection Direction;
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

            var raw = id / Data.GroundLength;
            var idInLine = id - raw * data.GroundLength;

            if (id % (data.GroundLength - 1) != 0 && id + 1 != data.GroundLength - 1)
            {
                RightNearbyElement = model.WorldElementModels[id + 1];
                ExistRightNearbyElement = true;
            }

            if (id % (data.GroundLength - 1) != 1 && id > 0)
            {
                LeftNearbyElement = model.WorldElementModels[id - 1];
                ExistLeftNearbyElement = true;
            }

            if (raw != 0)
            {
                TopNearbyElement = model.WorldElementModels[(raw - 1) * data.GroundLength + idInLine];
                ExistTopNearbyElement = true;
            }

            if (raw != data.GroundWidth - 1)
            {
                var dataGroundLength = (raw + 1) * data.GroundLength + idInLine;
                BottomNearbyElement = model.WorldElementModels[dataGroundLength];
                ExistBottomNearbyElement = true;
            }
        }

        #region Movement

        public void MoveLeft(PathTypes pathType = PathTypes.Straight)
        {
            Direction = PathDirection.Left;
            if (ExistLeftNearbyElement)
            {
                LeftNearbyElement.TransformObject(ObjectTypes.Path);
                LeftNearbyElement.PathElementModel.Direction = PathDirection.Left;
            }
            else
            {
                Update?.Invoke(PathTypes.End, 0);
            }
        }

        public void MoveRight(PathTypes pathType = PathTypes.Straight)
        {
            Direction = PathDirection.Right;
            if (ExistRightNearbyElement)
            {
                RightNearbyElement.TransformObject(ObjectTypes.Path);
                RightNearbyElement.PathElementModel.Direction = PathDirection.Right;
            }
            else
            {
                Update?.Invoke(PathTypes.End, 0);
            }
        }

        public void MoveTop(PathTypes pathType = PathTypes.Straight)
        {
            Direction = PathDirection.Top;
            if (ExistTopNearbyElement)
            {
                TopNearbyElement.TransformObject(ObjectTypes.Path);
                TopNearbyElement.PathElementModel.Direction = PathDirection.Top;
            }
            else
            {
                Update?.Invoke(PathTypes.End, 0);
            }
        }

        public void MoveBottom(PathTypes pathType = PathTypes.Straight)
        {
            Direction = PathDirection.Bottom;
            if (ExistBottomNearbyElement)
            {
                BottomNearbyElement.TransformObject(ObjectTypes.Path);
                BottomNearbyElement.PathElementModel.Direction = PathDirection.Bottom;
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
            Direction = PathDirection.Top;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[PathDirection.LeftToTop]);
        }

        public void RotateFromLeftToBottom()
        {
            Direction = PathDirection.Bottom;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[PathDirection.LeftToBottom]);
        }

        public void RotateFromRightToTop()
        {
            Direction = PathDirection.Top;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[PathDirection.RightToTop]);
        }

        public void RotateFromRightToBottom()
        {
            Direction = PathDirection.Bottom;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[PathDirection.RightToBottom]);
        }

        public void RotateFromTopToLeft()
        {
            Direction = PathDirection.Left;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[PathDirection.TopToLeft]);
        }

        public void RotateFromTopToRight()
        {
            Direction = PathDirection.Right;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[PathDirection.TopToRight]);
        }

        public void RotateFromBottomToLeft()
        {
            Direction = PathDirection.Left;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[PathDirection.BottomToLeft]);
        }

        public void RotateFromBottomToRight()
        {
            Direction = PathDirection.Right;
            Update?.Invoke(PathTypes.Bend, _anglesRotation[PathDirection.BottomToRight]);
        }

        #endregion

        public void SetStartPath()
        {
            Direction = PathDirection.None;
            Update?.Invoke(PathTypes.Cross, 0);
        }

        public void SetEndPath()
        {
            Update?.Invoke(PathTypes.End, _anglesStraight[Direction]);
            Direction = PathDirection.None;
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
                case PathDirection.Left:
                    return LeftNearbyElement?.PathElementModel;
                case PathDirection.Right:
                    return RightNearbyElement?.PathElementModel;
                case PathDirection.Top:
                    return TopNearbyElement?.PathElementModel;
                case PathDirection.Bottom:
                    return BottomNearbyElement?.PathElementModel;
            }

            return null;
        }
    }
}