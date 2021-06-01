using System.Collections.Generic;
using UnityEngine;
using Utilities;
using World.Experimental;
using World.Experimental.Systems;
using Random = UnityEngine.Random;

namespace World.Path
{
    public class PathGenerator : IWorldGenerator
    {
        public void Generate(GameContext context)
        {
            var xRandom = Random.Range(0, context.LocationData.X);
            var zRandom = Random.Range(0, context.LocationData.Z);
            var pathBlock = new PathBlock(context.BlockWorldModel.Blocks[new Vector3(xRandom, 0, zRandom)], Direction.None);
            
            context.BlockWorldModel.Blocks[new Vector3(xRandom, 0, zRandom)] = pathBlock;
     
            pathBlock.SetStartPath();

            var system = context.SystemCollection.Get<GeneratePathSystem>(SystemTypes.GeneratePathSystem);

            if (pathBlock.TryGetMoveDirection(context.BlockWorldModel, out var leftBlock, Direction.Left))
            {
                leftBlock.SetDefault();
                system.Add(leftBlock);
            }
            if (pathBlock.TryGetMoveDirection(context.BlockWorldModel, out var rightBlock, Direction.Right))
            {
                rightBlock.SetDefault();
                system.Add(rightBlock);
            }
            if (pathBlock.TryGetMoveDirection(context.BlockWorldModel, out var topBlock, Direction.Top))
            {
                topBlock.SetDefault();
                system.Add(topBlock);
            }
            if (pathBlock.TryGetMoveDirection(context.BlockWorldModel, out var bottomBlock, Direction.Bottom))
            {
                bottomBlock.SetDefault();
                system.Add(bottomBlock);
            }
        }
    }
}