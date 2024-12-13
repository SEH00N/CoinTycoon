using System.Collections.Generic;
using H00N.DataTables;
using Newtonsoft.Json;
using ProjectCoin.Farms;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ProjectCoin
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Initialize();
        }

        public async void Initialize()
        {
            AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>("DataTableJson");
            await handle.Task;
            TextAsset dataTableJsonData = handle.Result;

            Dictionary<string, string> jsonDatas = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataTableJsonData.text);
            DataTableManager.Initialize(jsonDatas);
        }

        private void OnApplicationQuit()
        {
            DataTableManager.Release();
        }
    }
}
