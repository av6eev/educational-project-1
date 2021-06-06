using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    [CreateAssetMenu(fileName = "New Game Data", menuName = "Game Data", order = 51)]
    public class LocationData : ScriptableObject
    {
        [Space][Header("World Settings")]
        public int X;
        public int Z;
        public bool HasPath;
        public bool HasTree;
        public bool HasCrop;
        public bool HasRiver;
        public bool HasLake;
        
        [Space][Header("Tree Settings")]
        public List<TreeTypes> TreeTypes;
        public int TreesCount;
        public float MinTreeSize;
        public float MaxTreeSize;
        
        [Space][Header("Path Settings")]
        public int ChanceToGeneratePath;
        public int PathRotationChance;

        [Space] [Header("Crop Settings")] 
        public List<CropTypes> CropTypes;
        public int CropsCount;
        
        [Space] [Header("River Settings")]
        public int RiversCount;
        public int ChanceToGenerateRiver;
        public int RiverRotationChance;
        
        [Space] [Header("Lake Settings")]
        public int LakesCount;
        public int LakeSize;
    }
}