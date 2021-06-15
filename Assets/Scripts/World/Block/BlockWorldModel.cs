using System.Collections.Generic;
using UnityEngine;
using World.Chunks;

namespace World.Block
{
    public class BlockWorldModel
    {
        public Dictionary<Vector3, BaseBlock> Blocks = new Dictionary<Vector3, BaseBlock>();
    }
}