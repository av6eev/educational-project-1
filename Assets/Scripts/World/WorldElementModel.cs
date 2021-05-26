using System;
using UnityEngine;
using Utilities;
using World.Cell;

namespace World
{
    public class WorldElementModel
    {
        public event Action ChangeObjectType;

        public int Angle;
        public float TreeSize;

        public PathWorldElementModel PathElementModel;

        public int Id { get; }
        public Vector3 Position { get; }

        public ObjectType ObjectType;
        public TreeTypes TreeTypes;

        public bool IsUsed { get; set; }
        public bool IsPath { get; private set; }

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

        public void SetTree(TreeTypes typeTree, float treeSize)
        {
            TreeTypes = typeTree;
            TreeSize = treeSize;
            ObjectType = ObjectType.Tree;
            IsUsed = true;
        }

        public void TransformObject(ObjectType type)
        {
            ObjectType = type;

            switch (type)
            {
                case ObjectType.Tree:
                    break;
                case ObjectType.Path:
                    IsPath = true;
                    break;
            }

            ChangeObjectType?.Invoke();
        }
    }
}