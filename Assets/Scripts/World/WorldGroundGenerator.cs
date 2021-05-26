using UnityEngine;
using Utilities;
using World.Cell;

namespace World
{
    public class WorldGroundGenerator : IWorldGenerator
    {
        public void Generate(WorldModel model)
        {
            var locationData = model.LocationData;
            var counter = 0;

            for (var i = 0; i < locationData.GroundLength; i++)
            {
                for (var j = 0; j < locationData.GroundWidth; j++)
                {
                    var cellModel = new WorldElementModel(counter,new Vector3(i, 0, j), locationData);
                    model.CellModels.Add(cellModel);
                    counter++;
                }
            }

            model.SearchNearby();
            
            for (var i = 0; i < locationData.TreesCount; i++)
            {
                var treeSize = Random.Range(locationData.MinTreeSize, locationData.MaxTreeSize);

                var typeTree = Random.Range(0, locationData.TreeTypes.Count);
                var spawnCell = Random.Range(0, locationData.GroundLength * locationData.GroundWidth);

                while (model.CellModels[spawnCell].IsUsed)
                {
                    spawnCell = Random.Range(0, locationData.GroundLength * locationData.GroundWidth);
                }

                model.CellModels[spawnCell].SetTree((TreeTypes) typeTree, treeSize);
            }
            
        }
    }
}