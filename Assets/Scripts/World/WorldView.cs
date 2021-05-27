using UnityEngine;
using World.Cell;

namespace World
{
    public class WorldView : MonoBehaviour
    {
        [SerializeField] public WorldElement Ground;
        [SerializeField] public WorldElement Tree;
        
        public WorldElementView Create(Vector3 worldElementPosition)
        {
            var go = Instantiate(Ground, worldElementPosition, Quaternion.identity);
            return new WorldElementView(go);
        }
    }
}