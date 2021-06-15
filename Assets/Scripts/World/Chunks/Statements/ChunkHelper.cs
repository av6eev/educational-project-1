using Utilities;

namespace World.Chunks.Statements
{
    public static class ChunkHelper
    {
        public static ChunkTypes GetChunkType(int x, int z)
        {
            if (x > 0 && z > 0)
            {
                return ChunkTypes.PP;
            }
            else if (x > 0 && z < 0)
            {
                return ChunkTypes.PM;
            }
            else if (x < 0 && z > 0)
            {
                return ChunkTypes.MP;
            }
            else if (x < 0 && z < 0)
            {
                return ChunkTypes.MM;
            }

            return ChunkTypes.Error;
        }
    }
}