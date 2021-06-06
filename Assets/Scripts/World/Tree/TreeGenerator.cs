using UnityEngine;
using Utilities;
using World.Block;
using World.Systems.Tree;
using World.Systems.Utilities;
using World.Utilities;

namespace World.Tree
{
    public class TreeGenerator : IWorldGenerator
    {
        public void Generate(GameContext context)
        {
            var locationData = context.LocationData;
            var blocks = context.BlockWorldModel.Blocks;
            var system = context.SystemCollection.Get<GenerateTreeSystem>(SystemTypes.GenerateTreeSystem);
            
            for (var i = 0; i < locationData.TreesCount; i++)
            {
                var x = Random.Range(0, locationData.X);
                var z = Random.Range(0, locationData.Z);
            
                var treeSize = Random.Range(locationData.MinTreeSize, locationData.MaxTreeSize);
                var typeTree = Random.Range(0, locationData.TreeTypes.Count);
                var treeBlock = new TreeBlock(blocks[new Vector3(x, 0, z)], treeSize, (TreeTypes)typeTree);
                
                treeBlock.Type = BlockType.Tree;
                blocks[new Vector3(x, 0, z)] = treeBlock;
                
                while (treeBlock.IsBorder || treeBlock.Type == BlockType.Path || treeBlock.Type == BlockType.Crop)
                {
                    x = Random.Range(0, locationData.X);
                    z = Random.Range(0, locationData.Z);

                    treeBlock = new TreeBlock(blocks[new Vector3(x, 0, z)], treeSize, (TreeTypes)typeTree);
                    blocks[new Vector3(x, 0, z)] = treeBlock;
                }

                system.Add(treeBlock);
            }
        }
    }
}