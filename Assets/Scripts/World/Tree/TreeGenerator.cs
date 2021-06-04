using UnityEngine;
using Utilities;
using World.Experimental;
using World.Experimental.Systems;
using World.Experimental.Systems.Tree;

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
                var xRandom = Random.Range(0, locationData.X);
                var zRandom = Random.Range(0, locationData.Z);
            
                var treeSize = Random.Range(locationData.MinTreeSize, locationData.MaxTreeSize);
                var typeTree = Random.Range(0, locationData.TreeTypes.Count);
                var treeBlock = new TreeBlock(blocks[new Vector3(xRandom, 0, zRandom)], treeSize, (TreeTypes)typeTree);
                
                treeBlock.Type = BlockType.Tree;
                blocks[new Vector3(xRandom, 0, zRandom)] = treeBlock;
                
                while (treeBlock.IsBorder || treeBlock.Type == BlockType.Path)
                {
                    xRandom = Random.Range(0, locationData.X);
                    zRandom = Random.Range(0, locationData.Z);

                    treeBlock = new TreeBlock(blocks[new Vector3(xRandom, 0, zRandom)], treeSize, (TreeTypes)typeTree);
                    blocks[new Vector3(xRandom, 0, zRandom)] = treeBlock;
                }

                system.Add(treeBlock);
            }
        }
    }
}