using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utilities;

namespace World.WorldElement
{
    public class WorldElementView
    {
        public readonly WorldElement WorldElement;
        private int _angle;
        private AsyncOperationHandle<GameObject> _assetAsync;
        public float Scale = 1;

        public WorldElementView(WorldElement worldElement)
        {
            WorldElement = worldElement;
        }

        public void ChangeObject(string id, int angle = 0)
        {
            _angle = angle;
            _assetAsync = Addressables.LoadAssetAsync<GameObject>(id);
            _assetAsync.Completed += OnSetObject;
        }

        private void OnSetObject(AsyncOperationHandle<GameObject> obj)
        {
            _assetAsync.Completed -= OnSetObject;
            var objResult = obj.Result;

            objResult.transform.rotation = Quaternion.Euler(0, _angle, 0);
            WorldElement.Set(objResult, Scale);
        }
    }
}