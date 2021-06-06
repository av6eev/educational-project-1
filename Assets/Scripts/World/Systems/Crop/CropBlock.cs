using System;
using UnityEngine;
using Utilities;
using World.Block;

namespace World.Systems.Crop
{
    public class CropBlock : BaseBlock
    {
        public CropTypes CropType;
        
        public CropBlock(int id, Vector3 position) : base(id, position)
        {
            base.Type = BlockType.Crop;
        }

        public CropBlock(BaseBlock block, CropTypes type) : base(block)
        {
            base.Type = BlockType.Crop;
            CropType = type;
        }

        public void Set(CropTypes type)
        {
            CropType = type;
        }
    }
}