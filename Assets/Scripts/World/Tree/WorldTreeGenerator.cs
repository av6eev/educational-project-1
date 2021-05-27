using UnityEngine;
using Utilities;
using World.Cell;

namespace World.Tree
{
    public class WorldTreeGenerator : IWorldGenerator
    {
        public void Generate(WorldModel model)
        {
            var locationData = model.LocationData;
            var counter = 0;

            for (var i = 0; i < locationData.TreesCount; i++)
            {
                var spawnPoint = Random.Range(0, model.WorldElementModels.Count);
                var worldElementModel = model.WorldElementModels[spawnPoint];
                var treeSize = Random.Range(locationData.MinTreeSize, locationData.MaxTreeSize);
                var typeTree = Random.Range(0, locationData.TreeTypes.Count);

                while (worldElementModel.IsUsed)
                {
                    spawnPoint = Random.Range(0, locationData.GroundLength * locationData.GroundWidth);
                }

                worldElementModel.TransformObject(ObjectTypes.Tree);
                // worldElementModel.SetTree((TreeTypes) typeTree, treeSize);
            }
        }
    }
}