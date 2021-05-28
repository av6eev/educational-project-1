using TMPro;
using UnityEngine;

namespace World.WorldElement
{
    public class WorldElement : MonoBehaviour
    {
        public Transform SpawnPoint;
        public MeshRenderer MeshRenderer;
        private GameObject _prefab;

        public void Set(GameObject obj, float scale = 1)
        {
            if (_prefab != null)
            {
                Destroy(_prefab);
            }
            
            _prefab = Instantiate(obj, SpawnPoint);
            _prefab.transform.localScale = new Vector3(scale, scale, scale);
        }

        public void DisableGround()
        {
            MeshRenderer.enabled = false;
        }
    }
}