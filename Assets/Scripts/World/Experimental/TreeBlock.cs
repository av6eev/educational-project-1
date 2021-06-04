using System;
using UnityEngine;
using Utilities;

namespace World.Experimental
{
    public class TreeBlock : Block
    {
        public event Action<TreeTypes> Update;
        public TreeTypes TreeType;
        public float TreeSize;
        
        public TreeBlock(int id, Vector3 position) : base(id, position)
        {
            Type = BlockType.Tree;
        }

        public TreeBlock(Block block, float treeSize, TreeTypes treeType) : base(block)
        {
            TreeSize = treeSize;
            TreeType = treeType;
            
            Type = BlockType.Tree;
        }

        public void Set(TreeTypes typeTree, float treeSize)
        {
            Update?.Invoke(typeTree);
            TreeType = typeTree;
            TreeSize = treeSize;
        }
    }
}