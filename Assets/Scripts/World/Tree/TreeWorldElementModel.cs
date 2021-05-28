using System;
using Utilities;

namespace World.Tree
{
    public class TreeWorldElementModel
    {
        public event Action<TreeTypes> Update;

        public LocationData Data;

        public float TreeSize;
        public TreeTypes TreeType;
        public bool IsUsed;

        public void SetTree(TreeTypes typeTree, float treeSize)
        {
            Update?.Invoke(typeTree);
            TreeType = typeTree;
            TreeSize = treeSize;
        }
    }
}