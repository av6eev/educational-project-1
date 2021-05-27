using System;
using UnityEngine;
using Utilities;
using World.Cell;
using World.Tree;

namespace World
{
    public class WorldElementModel
    {
        public event Action ChangeObjectType;
        
        public int Id { get; }
        public int Angle;
        public float TreeSize;
        public TreeTypes TreeTypes;
        public ObjectTypes ObjectTypes;

        public PathWorldElementModel PathElementModel;
        public TreeWorldElementModel TreeElementModel;

        public Vector3 Position { get; }

        public bool IsUsed { get; set; }
        public bool IsPath { get; private set; }
        public bool IsTree { get; private set; }

        public WorldElementModel(int id,Vector3 position, LocationData data)
        {
            Id = id;
            Position = position;

            if (data.HasGroundPath)
            {
                PathElementModel = new PathWorldElementModel();
            }
        }

        public void SearchNearby(WorldModel model, LocationData data)
        {
            PathElementModel.SearchNearby(model, Id, data);
        }

        public void TransformObject(ObjectTypes types)
        {
            ObjectTypes = types;

            switch (types)
            {
                case ObjectTypes.Tree:
                    IsTree = true;
                    break;
                case ObjectTypes.Path:
                    IsPath = true;
                    break;
            }

            ChangeObjectType?.Invoke();
        }
    }
}