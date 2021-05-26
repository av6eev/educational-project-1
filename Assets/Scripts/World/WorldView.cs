using UnityEngine;
using World.Cell;

namespace World
{
    public class WorldView : MonoBehaviour
    {
        [SerializeField] public WorldElement Ground;
        
        public WorldElementView Create(Vector3 cellPosition)
        {
            var go = Instantiate(Ground, cellPosition, Quaternion.identity);
            return new WorldElementView(go);
        }
    }
}