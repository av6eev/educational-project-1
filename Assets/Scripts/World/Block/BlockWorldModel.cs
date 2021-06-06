using System.Collections.Generic;
using UnityEngine;

namespace World.Block
{
    public class BlockWorldModel
    {
        public Dictionary<Vector3, BaseBlock> Blocks = new Dictionary<Vector3, BaseBlock>();
    }
}