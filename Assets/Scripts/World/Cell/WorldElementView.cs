using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utilities;

namespace World.Cell
{
    public class WorldElementView
    {
        private readonly WorldElement _go;
        private int _angle;
        private AsyncOperationHandle<GameObject> _assetAsync;
        
        public WorldElementView(WorldElement go)
        {
            _go = go;
        }

        public void ChangeObject(PathTypes type, int angle)
        {
            _angle = angle;
            _assetAsync = Addressables.LoadAssetAsync<GameObject>(ContentHelper.Pathes[type]);
            _assetAsync.Completed += OnCompleted;
        }
        

        private void OnCompleted(AsyncOperationHandle<GameObject> obj)
        {
            _assetAsync.Completed -= OnCompleted;
            var objResult = obj.Result;
            objResult.transform.rotation = Quaternion.Euler(0, _angle, 0);
            _go.Set(objResult);
        }
    }
}