using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utilities;

namespace World.Cell
{
    public class WorldElementView
    {
        public readonly WorldElement WorldElement;
        private int _angle;
        private AsyncOperationHandle<GameObject> _assetAsync;

        public WorldElementView(WorldElement worldElement)
        {
            WorldElement = worldElement;
        }

        public void ChangePathObject(PathTypes pathType, int angle)
        {
            _angle = angle;
            _assetAsync = Addressables.LoadAssetAsync<GameObject>(ContentHelper.Pathes[pathType]);
            _assetAsync.Completed += OnSetPath;
        }
        
        public void ChangeTreeObject(TreeTypes treeType)
        {
            _assetAsync = Addressables.LoadAssetAsync<GameObject>(ContentHelper.Trees[treeType]);
            _assetAsync.Completed += OnSetTree;
        }

        private void OnSetPath(AsyncOperationHandle<GameObject> obj)
        {
            _assetAsync.Completed -= OnSetPath;
            var objResult = obj.Result;

            objResult.transform.rotation = Quaternion.Euler(0, _angle, 0);
            WorldElement.Set(objResult);
        }
        
        private void OnSetTree(AsyncOperationHandle<GameObject> obj)
        {
            _assetAsync.Completed -= OnSetTree;
            var objResult = obj.Result;
            
            WorldElement.Set(objResult);
        }
    }
}