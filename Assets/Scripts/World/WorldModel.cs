using System.Collections.Generic;
using Utilities;

namespace World
{
    public class WorldModel
    {
        public LocationData LocationData;
        public List<WorldElementModel> WorldElementModels = new List<WorldElementModel>();
        public WorldElementModel this[int id] => WorldElementModels[id];

        public WorldModel(LocationData data)
        {
            LocationData = data;
        }

        public void SearchNearby()
        {
            foreach (var worldElementModel in WorldElementModels)
            {
                worldElementModel.SearchNearby(this, LocationData);
            }
        }
    }
}