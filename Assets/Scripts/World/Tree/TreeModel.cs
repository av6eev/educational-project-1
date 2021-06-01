using System;
using Utilities;

namespace World.Tree
{
    public class TreeWorldElementModel
    {
        public event Action<TreeTypes, float> Update;

        public LocationData Data;

        public float TreeSize;
        public TreeTypes TreeType;
        public bool IsUsed;

        public void SetTree(TreeTypes typeTree, float treeSize)
        {
            Update?.Invoke(typeTree, treeSize);
            TreeType = typeTree;
            TreeSize = treeSize;
        }
    }
}