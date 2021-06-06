using UnityEngine;
using Utilities;
using World.Block;
using World.Systems.Path;
using World.Systems.River;
using World.Systems.Utilities;
using World.Utilities;

namespace World.River
{
    public class RiverGenerator : IWorldGenerator
    {
        public void Generate(GameContext context)
        {
            for (var i = 0; i < context.LocationData.RiversCount; i++)
            {
                var x = Random.Range(0, context.LocationData.X);
                var z = Random.Range(0, context.LocationData.Z);
                var riverBlock = new RiverBlock(context.BlockWorldModel.Blocks[new Vector3(x, 0, z)], Direction.None);

                riverBlock.Type = BlockType.River;
                
                context.BlockWorldModel.Blocks[new Vector3(x, 0, z)] = riverBlock;

                while (riverBlock.IsBorder || riverBlock.IsCrop || riverBlock.IsTree || riverBlock.IsPath)
                {
                    x = Random.Range(0, context.LocationData.X);
                    z = Random.Range(0, context.LocationData.Z);
                    riverBlock = new RiverBlock(context.BlockWorldModel.Blocks[new Vector3(x, 0, z)], Direction.None);

                    context.BlockWorldModel.Blocks[new Vector3(x, 0, z)] = riverBlock;
                }

                riverBlock.SetStartPath();

                var system = context.SystemCollection.Get<GenerateRiverSystem>(SystemTypes.GenerateRiverSystem);

                if (riverBlock.TryGetMoveDirection(context.BlockWorldModel, out var leftBlock, Direction.Left))
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

                if (riverBlock.TryGetMoveDirection(context.BlockWorldModel, out var rightBlock, Direction.Right))
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

                if (riverBlock.TryGetMoveDirection(context.BlockWorldModel, out var topBlock, Direction.Top))
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

                if (riverBlock.TryGetMoveDirection(context.BlockWorldModel, out var bottomBlock, Direction.Bottom))
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
}