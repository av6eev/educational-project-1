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

        public LocationData Data;
        public PathTypes PathTypes;
        public PathDirection Direction = PathDirection.None;
        public int Id;
        public bool IsUsed;
        public WorldElementModel LeftNearbyElement;
        public WorldElementModel RightNearbyElement;
        public WorldElementModel TopNearbyElement;
        public WorldElementModel BottomNearbyElement;

        public bool ExistLeftNearbyElement;
        public bool ExistRightNearbyElement;
        public bool ExistTopNearbyElement;
        public bool ExistBottomNearbyElement;

        public void SearchNearby(WorldModel model, int id, LocationData data)
        {
            Id = id;
            Data = data;

            var raw = id / Data.GroundLength;
            var idInLine = id - raw * data.GroundLength;

            if (id % (data.GroundLength - 1) != 0 && id + 1 != data.GroundLength - 1)
            {
                RightNearbyElement = model.CellModels[id + 1];
                ExistRightNearbyElement = true;
            }

            if (id % (data.GroundLength - 1) != 1 && id > 0)
            {
                LeftNearbyElement = model.CellModels[id - 1];
                ExistLeftNearbyElement = true;
            }

            if (raw != 0)
            {
                TopNearbyElement = model.CellModels[(raw - 1) * data.GroundLength + idInLine];
                ExistTopNearbyElement = true;
            }

            if (raw != data.GroundWidth - 1)
            {
                var dataGroundLength = (raw + 1) * data.GroundLength + idInLine;
                BottomNearbyElement = model.CellModels[dataGroundLength];
                ExistBottomNearbyElement = true;
            }
        }

        public void MoveLeft(PathTypes pathType = PathTypes.Straight)
        {
            Direction = PathDirection.Left;
            if (ExistLeftNearbyElement)
            {
                LeftNearbyElement.TransformObject(ObjectType.Path);
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
                RightNearbyElement.TransformObject(ObjectType.Path);
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
                TopNearbyElement.TransformObject(ObjectType.Path);
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
                BottomNearbyElement.TransformObject(ObjectType.Path);
                BottomNearbyElement.PathElementModel.Direction = PathDirection.Bottom;
            }
            else
            {
                Update?.Invoke(PathTypes.End, 0);
            }
        }

        public void RotateLeft(PathTypes pathType = PathTypes.Straight)
        {
            Direction = PathDirection.Left;
            Update?.Invoke(PathTypes.Bend,  _angles2[Direction]);
        }

        public void RotateRight(PathTypes pathType = PathTypes.Straight)
        {
            Direction = PathDirection.Right;
            Update?.Invoke(PathTypes.Bend,  _angles2[Direction]);
        }

        public void RotateTop(PathTypes pathType = PathTypes.Straight)
        {
            Direction = PathDirection.Top;
            Update?.Invoke(PathTypes.Bend,  _angles2[Direction]);
        }

        public void RotateBottom(PathTypes pathType = PathTypes.Straight)
        {
            Direction = PathDirection.Bottom;
            Update?.Invoke(PathTypes.Bend,  _angles2[Direction]);
        }

        public void SetStartPath()
        {
            Direction = PathDirection.None;
            Update?.Invoke(PathTypes.Cross, 0);
        }

        private static Dictionary<PathDirection, int> _angles = new Dictionary<PathDirection, int>()
        {
            {PathDirection.Right, 0},
            {PathDirection.Left, -180},
            {PathDirection.Top, -90},
            {PathDirection.Bottom, 90},
            {PathDirection.None, 0},
        };     
        private static Dictionary<PathDirection, int> _angles2 = new Dictionary<PathDirection, int>()
        {
            {PathDirection.Right, 180},
            {PathDirection.Left, -180},
            {PathDirection.Top, -90},
            {PathDirection.Bottom, 0},
            {PathDirection.None, 0},
        };

        public void SetEndPath()
        {
            Update?.Invoke(PathTypes.End, _angles[Direction]);
            Direction = PathDirection.None;
        }

        public PathWorldElementModel GetDirectionModel()
        {
            switch (Direction)
            {
                case PathDirection.Left:
                    return LeftNearbyElement?.PathElementModel;
                case PathDirection.Top:
                    return TopNearbyElement?.PathElementModel;
                case PathDirection.Bottom:
                    return BottomNearbyElement?.PathElementModel;
                case PathDirection.Right:
                    return RightNearbyElement?.PathElementModel;
            }

            return null;
        }
        
        public void SetDefault()
        {
            Update?.Invoke(PathTypes.Straight, _angles[Direction]);
        }
    }
}