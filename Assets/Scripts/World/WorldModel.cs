using System.Collections.Generic;
using Utilities;

namespace World
{
    public class WorldModel
    {
        public LocationData LocationData;
        public List<WorldElementModel> CellModels = new List<WorldElementModel>();

        public WorldElementModel this[int id] => CellModels[id];

        public WorldModel(LocationData data)
        {
            LocationData = data;
        }

        public void SearchNearby()
        {
            foreach (var cellModel in CellModels)
            {
                cellModel.SearchNearby(this, LocationData);
            }
        }
    }
}