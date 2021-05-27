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
                    var worldElementModel = new WorldElementModel(counter,new Vector3(i, 0, j), locationData);
                    model.WorldElementModels.Add(worldElementModel);
                    counter++;
                }
            }

            model.SearchNearby();
        }
    }
}