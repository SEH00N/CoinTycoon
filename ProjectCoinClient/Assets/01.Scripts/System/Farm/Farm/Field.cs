using System;
using Cysharp.Threading.Tasks;
using H00N.DataTables;
using H00N.Resources.Pools;
using ProjectCoin.Datas;
using ProjectCoin.DataTables;
using ProjectCoin.Networks;
using ProjectCoin.Networks.Payloads;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectCoin.Farms
{
    public class Field : FarmerTargetableBehaviour
    {
        [SerializeField] int fieldID = 0;

        public event Action<EFieldState> OnStateChangedEvent = null;
        public event Action<int> OnGrowUpEvent = null;

        private CropSO currentCropData = null;
        public CropSO CurrentCropData => currentCropData;

        // 나중엔 유저 정보에 갖고 있게 해야함
        private EFieldState currentState = EFieldState.None;
        public EFieldState CurrentState => currentState;

        private bool requestWaiting = false;
        private bool postponeTick = false;

        public override bool TargetEnable => !requestWaiting && CurrentState != EFieldState.Growing;
        private int growth = 0;

        protected override void Awake()
        {
            base.Awake();
            ChangeState(EFieldState.Fallow);
        }

        public void SetCropData(CropSO cropData)
        {
            currentCropData = cropData;
        }

        public void Plant()
        {
            if (requestWaiting)
                return;

            PlantRequest payload = new PlantRequest(currentCropData.id, fieldID);
            NetworkManager.Instance.SendWebRequest<PlantResponse>(payload, HandlePlantResponse);
            requestWaiting = true;
        }

        private void HandlePlantResponse(PlantResponse res)
        {
            requestWaiting = false;
            if (res.networkResult != ENetworkResult.Success)
                return;

            growth = -1;
            ChangeState(EFieldState.Dried);
            GrowUp();

            DateManager.Instance.OnTickCycleEvent += HandleTickCycleEvent;
        }

        public void Harvest()
        {
            if (requestWaiting)
                return;

            DateManager.Instance.OnTickCycleEvent -= HandleTickCycleEvent;

            HarvestRequest payload = new HarvestRequest(fieldID);
            NetworkManager.Instance.SendWebRequest<HarvestResponse>(payload, HandleHarvestResponse);
            requestWaiting = true;
        }

        private async void HandleHarvestResponse(HarvestResponse res)
        {
            requestWaiting = false;
            if (res.networkResult != ENetworkResult.Success)
                return;

            ItemTableRow tableRow = DataTableManager.GetTable<ItemTable>().GetRow(currentCropData.TableRow.productCropID);
            if (tableRow != null)
            {
                Vector3 randomOffset = Random.insideUnitCircle * 3f;
                Vector3 itemPosition = TargetPosition + randomOffset;

                Item item = await PoolManager.SpawnAsync(tableRow.itemType.ToString()) as Item;
                item.transform.position = itemPosition;
                item.Initialize(tableRow.id).Forget();
            }

            ChangeState(EFieldState.Fallow);
        }

        private void HandleTickCycleEvent()
        {
            if (postponeTick)
            {
                postponeTick = false;
                return;
            }

            if (currentState != EFieldState.Growing)
                return;

            ChangeState(EFieldState.Dried);
            GrowUp();
        }

        private void GrowUp()
        {
            growth++;
            if (growth % currentCropData.TableRow.growthRate != 0)
                return;

            int currentStep = growth / currentCropData.TableRow.growthRate;
            OnGrowUpEvent?.Invoke(currentStep);

            if (currentStep >= currentCropData.TableRow.growthStep - 1)
                ChangeState(EFieldState.Fruition);
        }

        public void ChangeState(EFieldState targetState)
        {
            EFieldState prevState = currentState;
            currentState = targetState;
            if (prevState != currentState)
                OnStateChangedEvent?.Invoke(currentState);

            postponeTick = true;
        }
    }
}
