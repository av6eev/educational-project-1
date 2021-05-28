using System;
using UnityEngine;
using Utilities;
using World.Tree;
using World.WorldElement;

namespace World
{
    public class WorldView : MonoBehaviour
    {
        [SerializeField] public WorldElement.WorldElement Ground;
        
        public WorldElementView CreateGround(Vector3 worldElementPosition)
        {
            var go = Instantiate(Ground, worldElementPosition, Quaternion.identity);
            return new WorldElementView(go);
        }
    }
}