using UnityEngine;
using Utilities;
using World.Block;

namespace World.Systems.Lake
{
    public class LakeBlock : BaseBlock
    {
        public LakeTypes LakeType;
        public AngleDirection AngleDirection;
        
        public LakeBlock(int id, Vector3 position) : base(id, position)
        {
            Type = BlockType.Lake;
        }

        public LakeBlock(BaseBlock block, LakeTypes lakeType) : base(block)
        {
            Type = BlockType.Lake;
            LakeType = lakeType;
        }

        public void SetOpen()
        {
            LakeType = LakeTypes.Open;
        }

        public void SetCorner()
        {
            LakeType = LakeTypes.Corner;
        }

        public void SetSide()
        {
            LakeType = LakeTypes.Side;
        }

        public int GetAngle()
        {
            switch (LakeType)
            {
                case LakeTypes.Corner:
                    break;
                case LakeTypes.Open:
                    break;
                case LakeTypes.Side:
                    break;
            }

            return 0;
        }
    }
}