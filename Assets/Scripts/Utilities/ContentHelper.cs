using System.Collections.Generic;

namespace Utilities
{
    public static class ContentHelper
    {
        public static Dictionary<TreeTypes, string> Trees = new Dictionary<TreeTypes, string>()
        {
            {TreeTypes.PlateauDark, "tree_plateau_dark"},
            {TreeTypes.PineSmall, "tree_pineSmallA"},
            {TreeTypes.Thin, "tree_thin"},
            {TreeTypes.Tall, "tree_tall"},
            {TreeTypes.Palm, "tree_palm"}
        };
        
        public static Dictionary<PathTypes, string> Pathes = new Dictionary<PathTypes, string>()
        {
            {PathTypes.Bend, "ground_pathBend"},
            {PathTypes.Cross, "ground_pathCross"},
            {PathTypes.End, "ground_pathEnd"},
            {PathTypes.StraightWithRocks, "ground_pathRocks"},
            {PathTypes.Straight, "ground_pathStraight"}
        };
    }
}