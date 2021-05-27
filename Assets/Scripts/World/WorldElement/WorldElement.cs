using TMPro;
using UnityEngine;

namespace World
{
    public class WorldElement : MonoBehaviour
    {
        public Transform SpawnPoint;
        public MeshRenderer MeshRenderer;
        public TextMeshProUGUI Text;
        private GameObject _prefab;

        public void Set(GameObject obj)
        {
            if (_prefab != null)
            {
                Destroy(_prefab);
            }
            
            MeshRenderer.enabled = false;
            _prefab = Instantiate(obj, SpawnPoint);
        }

        public void SetId(int id)
        {
            Text.text = id.ToString();
        }
    }
}