using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    [CreateAssetMenu(fileName = "New Game Data", menuName = "Game Data", order = 51)]
    public class LocationData : ScriptableObject
    {
        [Space][Header("Ground")]
        public int GroundLength;
        public int GroundWidth;
        public bool HasGroundPath;
        
        [Space][Header("Tree Settings")]
        public List<TreeTypes> TreeTypes;
        public int TreesCount;
        public float MinTreeSize;
        public float MaxTreeSize;
        
        [Space][Header("Path Settings")]
        public int ChanceToGeneratePath;
        public int RotationChance;
    }
}