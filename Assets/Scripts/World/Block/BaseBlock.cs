using UnityEngine;

namespace World.Block
{
    public class BaseBlock : IBlock
    {
        public int Id;
        public Vector3 Position;
        public Vector3 LeftBlock;
        public Vector3 RightBlock;
        public Vector3 TopBlock;
        public Vector3 BottomBlock;
        public BlockType Type;

        public bool IsBorder;
        public bool IsPath;
        public bool IsTree;
        public bool IsCrop;
        public bool IsRiver;
        public bool IsLake;

        public BaseBlock(int id, Vector3 position)
        {
            Id = id;
            Position = position;

            RightBlock = new Vector3(position.x,0, position.z + 1);
            LeftBlock = new Vector3(position.x, 0, position.z - 1);
            TopBlock = new Vector3(position.x - 1, 0, position.z);
            BottomBlock = new Vector3(position.x + 1, 0, position.z);
        }

        public BaseBlock(BaseBlock block)
        {
            IsBorder = block.IsBorder;
            IsPath = block.IsPath;
            IsTree = block.IsTree;
            
            Id = block.Id;
            
            Position = block.Position;
            RightBlock = block.RightBlock;
            LeftBlock = block.LeftBlock;
            TopBlock = block.TopBlock;
            BottomBlock = block.BottomBlock;
        }
    }
}