using World;
using World.Block;
using World.Chunks;
using World.Systems.Utilities;

namespace Utilities
{
    public class GameContext
    {
        public BlockWorldModel BlockWorldModel;
        public ChunkModel ChunkModel;
        
        public LocationData LocationData;

        public SystemCollection SystemCollection;
    }
}