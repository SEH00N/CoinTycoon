using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace H00N.Resources
{
    [System.Serializable]
    public class AddressableAsset<T> where T : Object
    {
#if UNITY_EDITOR
        [SerializeField] private T reference;
#endif

        [SerializeField] private string key = null;
        private T asset = null;
        public T Asset => asset;

        public static implicit operator T(AddressableAsset<T> reference) => reference.Asset;
        // public static implicit operator Object(AddressableAsset<T> reference) => reference.Asset;

        public void Initialize()
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("Key is null or empty.");
                return;
            }

            asset = ResourceManager.LoadResource<T>(key);
        }

        public async Task InitializeAsync()
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("Key is null or empty.");
                return;
            }

            asset = await ResourceManager.LoadResourceAsync<T>(key);
        }
    }
}
