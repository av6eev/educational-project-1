using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Utilities;
using World.Block;
using World.Chunks.Statements;

namespace World.Chunks
{
    public class Chunk
    {
        public string Id = string.Empty;
        public ChunkTypes ChunkType;
        public List<BaseBlock> Blocks = new List<BaseBlock>(20);

        public Chunk(int x, int z, ChunkTypes chunkType)
        {
            ChunkType = chunkType;

            switch (ChunkType)
            {
                case ChunkTypes.PP:
                    Id = $"PP_{x}x{z}z";
                    break;
                case ChunkTypes.MP:
                    Id = $"MP_{x}x{z}z";
                    break;
                case ChunkTypes.MM:
                    Id = $"MM_{x}x{z}z";
                    break;
                case ChunkTypes.PM:
                    Id = $"PM_{x}x{z}z";
                    break;
            }
            
            Load();
        }

        private async void Load()
        {
            var path = Application.dataPath + "/World/" + ChunkType + "/" + Id + ".json";

            if (File.Exists(path))
            {
                using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                using var reader = new StreamReader(fileStream);
                await reader.ReadToEndAsync();
            }
            else
            {
                File.Create(path).Dispose();
            }
        }

        public async void Unload()
        {
            var path = Application.dataPath + "/World/" + ChunkType + "/" + Id + ".json";

            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Write);
            using var writer = new StreamWriter(fileStream);
            await writer.WriteAsync("");
        }
    }
}