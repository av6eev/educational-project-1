using Utilities;
using World.WorldElement;

namespace World.Chunks
{
    public class ChunkController : IController
    {
        private readonly Chunk _model;
        private readonly WorldElementView _view;

        public ChunkController(Chunk model, WorldElementView view)
        {
            _model = model;
            _view = view;
        }
        
        public void Activate()
        {
        }

        public void Deactivate()
        {
        }
    }
}