using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using World.Block;
using World.Systems.Lake;
using World.Utilities;
using Random = UnityEngine.Random;

namespace World.Lake
{
    public class LakeGenerator : IWorldGenerator
    {
        private Dictionary<int, LakeBlock> _lakeBlocks = new Dictionary<int, LakeBlock>();
        
        public void Generate(GameContext context)
        {
            var data = context.LocationData;
            var blocks = context.BlockWorldModel.Blocks;
            
            for (var k = 0; k < data.LakesCount; k++)
            {
                var lakeBlock = CreateSpawnPoint(context, data, blocks);
                CreateLake(lakeBlock, data);
            }
        }

        private static LakeBlock CreateSpawnPoint(GameContext context, LocationData data, Dictionary<Vector3, BaseBlock> blocks)
        {
            var x = Random.Range(0, data.X);
            var z = Random.Range(0, data.Z);
            var spawnPoint = new LakeBlock(blocks[new Vector3(x, 0, z)], LakeTypes.Open);

            spawnPoint.Type = BlockType.Lake;
            context.BlockWorldModel.Blocks[new Vector3(x, 0, z)] = spawnPoint;

            while (spawnPoint.IsBorder || spawnPoint.IsCrop || spawnPoint.IsPath || spawnPoint.IsTree || spawnPoint.IsRiver)
            {
                x = Random.Range(0, data.X);
                z = Random.Range(0, data.Z);
                spawnPoint = new LakeBlock(blocks[new Vector3(x, 0, z)], LakeTypes.Open);

                spawnPoint.Type = BlockType.Lake;
                context.BlockWorldModel.Blocks[new Vector3(x, 0, z)] = spawnPoint;
            }

            return spawnPoint;
        }

        private void CreateLake(LakeBlock spawnPoint, LocationData data)
        {
            var counter = 0;

            spawnPoint.SetOpen();

            for (var i = 0; i < data.LakeSize; i++)
            {
                for (var j = 0; j < data.LakeSize; j++)
                {
                    _lakeBlocks.Add(counter, new LakeBlock(counter, new Vector3(i, 0, j)));
                        
                    if ((i == 0 && j > 0 && j < data.LakeSize) || (i == data.LakeSize && j > 0 && j < data.LakeSize))
                    {
                        _lakeBlocks[counter].SetSide();
                    }
                    else if ((j == 0 && i > 0 && i < data.LakeSize) || (j == data.LakeSize && i > 0 && i < data.LakeSize))
                    {
                        _lakeBlocks[counter].SetSide();
                    }
                    else if (i == 0 || i == data.LakeSize || j == 0 || j == data.LakeSize)
                    {
                        _lakeBlocks[counter].SetCorner();
                    }
                    else
                    {
                        _lakeBlocks[counter].SetOpen();
                    }

                    counter++;
                }
            }
        }
    }
}