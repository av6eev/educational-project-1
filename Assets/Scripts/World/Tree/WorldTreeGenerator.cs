// using UnityEngine;
// using Utilities;
//
// namespace World.Tree
// {
//     public class WorldTreeGenerator : IWorldGenerator
//     {
//         public void Generate(WorldModel model)
//         {
//             var locationData = model.LocationData;
//             var spawnPoint = Random.Range(0, model.WorldElementModels.Count);
//
//             for (var i = 0; i < locationData.TreesCount; i++)
//             {
//                 var treeSize = Random.Range(locationData.MinTreeSize, locationData.MaxTreeSize);
//                 var typeTree = Random.Range(0, locationData.TreeTypes.Count);
//                 
//                 while (model.WorldElementModels[spawnPoint].IsUsed || model.WorldElementModels[spawnPoint].IsPath)
//                 {
//                     spawnPoint = Random.Range(0, locationData.X * locationData.Z);
//                 }
//
//                 model.WorldElementModels[spawnPoint].TransformObject(ObjectTypes.Tree);
//                 model.WorldElementModels[spawnPoint].TreeElementModel.SetTree((TreeTypes) typeTree, treeSize);
//             }
//         }
//     }
// }