using UnityEngine;
using Utilities;
using World.Experimental;
using World.WorldElement;

namespace World
{
    public class GroundGenerator : IWorldGenerator
    {
        public void Generate(GameContext context)
        {
            var locationData = context.LocationData;
            var counter = 0;

            for (var i = 0; i < locationData.X; i++)
            {
                for (var j = 0; j < locationData.Z; j++)
                {
                    var position = new Vector3(i, 0, j);
                    var block = new Block(counter, position);
                    
                    context.BlockWorldModel.Blocks.Add(position, block);

                    if (i == 0 || i == locationData.X - 1 || j == 0 || j == locationData.Z - 1)
                    {
                        block.IsBorder = true;
                    }
                    
                    counter++;
                }
            }
        }
    }
}