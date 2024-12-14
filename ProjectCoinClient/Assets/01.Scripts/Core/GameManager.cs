using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using H00N.DataTables;
using Newtonsoft.Json;
using ProjectCoin.Farms;
using ProjectCoin.Networks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ProjectCoin
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance = null;
        public static GameManager Instance => instance;

        private void Awake()
        {
            if(instance != null)
                DestroyImmediate(instance.gameObject);

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public async UniTask InitializeAsync()
        {
            AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>("DataTableJson");
            await handle.Task;
            TextAsset dataTableJsonData = handle.Result;

            Dictionary<string, string> jsonDatas = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataTableJsonData.text);
            DataTableManager.Initialize(jsonDatas);

            new NetworkManager();
        }

        private void OnApplicationQuit()
        {
            DataTableManager.Release();
        }
    }
}
