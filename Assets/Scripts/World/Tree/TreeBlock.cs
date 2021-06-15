using System;
using UnityEngine;
using Utilities;
using World.Block;

namespace World.Tree
{
    public class TreeBlock : BaseBlock
    {
        public event Action<TreeTypes> Update;
        public TreeTypes TreeType;
        public float Size;
        
        public TreeBlock(int id, Vector3 position) : base(id, position)
        {
            base.Type = BlockType.Tree;
        }

        public TreeBlock(BaseBlock block, float size, TreeTypes treeType) : base(block)
        {
            Size = size;
            TreeType = treeType;
            
            base.Type = BlockType.Tree;
        }

        public void Set(TreeTypes type, float size)
        {
            Update?.Invoke(type);
            TreeType = type;
            Size = size;
        }
    }
}