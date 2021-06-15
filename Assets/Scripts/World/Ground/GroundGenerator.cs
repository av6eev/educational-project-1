using UnityEngine;
using Utilities;
using World.Block;
using World.Utilities;

namespace World.Ground
{
    public class GroundGenerator : IWorldGenerator
    {
        public void Generate(GameContext context)
        {
            var data = context.LocationData;
            var counter = 0;

            for (var i = 0; i < data.X; i++)
            {
                for (var j = 0; j < data.Z; j++)
                {
                    
                }
            }
        }
    }
}