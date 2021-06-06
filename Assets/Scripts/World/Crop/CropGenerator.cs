using UnityEngine;
using Utilities;
using World.Block;
using World.Systems.Crop;
using World.Systems.Utilities;
using World.Utilities;

namespace World.Crop
{
    public class CropGenerator : IWorldGenerator
    {
        public void Generate(GameContext context)
        {
            var locationData = context.LocationData;
            var blocks = context.BlockWorldModel.Blocks;
            var system = context.SystemCollection.Get<GenerateCropSystem>(SystemTypes.GenerateCropSystem);

            for (var i = 0; i < locationData.CropsCount; i++)
            {
                var x = Random.Range(0, locationData.X);
                var z = Random.Range(0, locationData.Z);

                var cropType = Random.Range(0, locationData.CropTypes.Count);
                var cropBlock = new CropBlock(blocks[new Vector3(x, 0, z)], (CropTypes) cropType);

                cropBlock.Type = BlockType.Crop;
                blocks[new Vector3(x, 0, z)] = cropBlock;

                while (cropBlock.IsBorder || cropBlock.Type == BlockType.Path || cropBlock.Type == BlockType.Tree)
                {
                    x = Random.Range(0, locationData.X);
                    z = Random.Range(0, locationData.Z);

                    cropBlock = new CropBlock(blocks[new Vector3(x, 0, z)], (CropTypes) cropType);
                    blocks[new Vector3(x, 0, z)] = cropBlock;
                }

                system.Add(cropBlock);
            }
        }
    }
}