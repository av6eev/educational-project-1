using System;
using UnityEngine;
using Utilities;
using World.Block;
using World.Chunks;
using World.Chunks.Statements;
using World.Systems.Utilities;

namespace World.Systems.Chunks
{
    public class GenerateChunkSystem : ISystem
    {
        private readonly ChunkModel _model;
        private readonly GameContext _context;
        private readonly Action _endGeneration;

        public GenerateChunkSystem(ChunkModel model, GameContext context, Action endGeneration)
        {
            _model = model;
            _context = context;
            _endGeneration = endGeneration;
        }

        public void Update()
        {
            var data = _context.LocationData;
            var size = data.ChunkSize;
            var x = 1;
            var z = 1;
            var n = 0;
            var m = 0;
            var counter = 0;

            for (var i = 0; i < data.X; i++)
            {
                for (var j = 0; j < data.Z; j++)
                {
                    var position = new Vector3(i, 0, j);
                    var block = new BaseBlock(counter, position);
                    
                    block.Type = BlockType.Ground;
                    _context.BlockWorldModel.Blocks.Add(position, block);

                    if (i == 0 || i == data.X - 1 || j == 0 || j == data.Z - 1)
                    {
                        block.IsBorder = true;
                    }
                    
                    if (i / size > m && i / size != 0)
                    {
                        x = i / size + 1;
                        m++;
                    }
                    else if (j / size > n && j / size != 0)
                    {
                        z = j / size + 1;
                        n++;
                    }
                    else if (j / size == 0)
                    {
                        z = 1;
                        n = 0;
                    }

                    var newChunk = new Chunk(x, z, ChunkHelper.GetChunkType(x, z));
                    
                    if (!_context.ChunkModel.Chunks.ContainsKey(newChunk.Id))
                    {
                        _context.ChunkModel.Chunks.Add(newChunk.Id, newChunk);                    
                    }
                    
                    counter++;
                }
            }

            _endGeneration();
        }
    }
}