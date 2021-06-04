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

            while (pathBlock.IsBorder)
            {
                xRandom = Random.Range(0, context.LocationData.X);
                zRandom = Random.Range(0, context.LocationData.Z);
                pathBlock = new PathBlock(context.BlockWorldModel.Blocks[new Vector3(xRandom, 0, zRandom)], Direction.None);

                context.BlockWorldModel.Blocks[new Vector3(xRandom, 0, zRandom)] = pathBlock;
            }
            
            pathBlock.SetStartPath();

            var system = context.SystemCollection.Get<GeneratePathSystem>(SystemTypes.GeneratePathSystem);

            if (pathBlock.TryGetMoveDirection(context.BlockWorldModel, out var leftBlock, Direction.Left))
            {
                if (leftBlock.IsBorder)
                {
                    leftBlock.SetEndPath();
                }
                else
                {
                    leftBlock.SetDefault();
                }

                system.Add(leftBlock);
            }

            if (pathBlock.TryGetMoveDirection(context.BlockWorldModel, out var rightBlock, Direction.Right))
            {
                if (rightBlock.IsBorder)
                {
                    rightBlock.SetEndPath();
                }
                else
                {
                    rightBlock.SetDefault();
                }

                system.Add(rightBlock);
            }

            if (pathBlock.TryGetMoveDirection(context.BlockWorldModel, out var topBlock, Direction.Top))
            {
                if (topBlock.IsBorder)
                {
                    topBlock.SetEndPath();
                }
                else
                {
                    topBlock.SetDefault();
                }

                system.Add(topBlock);
            }

            if (pathBlock.TryGetMoveDirection(context.BlockWorldModel, out var bottomBlock, Direction.Bottom))
            {
                if (bottomBlock.IsBorder)
                {
                    bottomBlock.SetEndPath();
                }
                else
                {
                    bottomBlock.SetDefault();
                }

                system.Add(bottomBlock);
            }
        }
    }
}