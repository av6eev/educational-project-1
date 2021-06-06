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
            {PathTypes.Straight, "ground_pathStraight"},
            {PathTypes.Bridge, "bridge_woodNarrow"}
        };

        public static Dictionary<CropTypes, string> Crops = new Dictionary<CropTypes, string>()
        {
            {CropTypes.Bamboo, "crops_bambooStageB"},
            {CropTypes.Carrot, "crop_carrot"},
            {CropTypes.Corn, "crops_cornStageD"},
            {CropTypes.Grass, "grass_large"},
            {CropTypes.Melon, "crop_melon"},
            {CropTypes.Pumpkin, "crop_pumpkin"},
            {CropTypes.Turnip, "crop_turnip"},
            {CropTypes.FlowerPurple, "flower_purpleA"},
            {CropTypes.FlowerRed, "flower_redB"},
            {CropTypes.FlowerYellow, "flower_yellowC"},
            {CropTypes.MushroomRed, "mushroom_redGroup"},
            {CropTypes.MushroomTan, "mushroom_tanTall"}
        };

        public static Dictionary<RiverTypes, string> Rivers = new Dictionary<RiverTypes, string>()
        {
            {RiverTypes.Bend, "ground_riverBendBank"},
            {RiverTypes.Cross, "ground_riverCross"},
            {RiverTypes.End, "ground_riverEndClosed"},
            {RiverTypes.StraightWithRocks, "ground_riverRocks"},
            {RiverTypes.Straight, "ground_riverStraight"}
        };
        
        public static Dictionary<LakeTypes, string> Lakes = new Dictionary<LakeTypes, string>()
        {
            {LakeTypes.Corner, "ground_riverCorner"},  
            {LakeTypes.Open, "ground_riverOpen"},  
            {LakeTypes.Side, "ground_riverSide"}  
        };
    }
}