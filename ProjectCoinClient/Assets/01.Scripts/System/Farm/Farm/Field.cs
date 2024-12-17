using System;
using ProjectCoin.Datas;
using ProjectCoin.Networks;
using ProjectCoin.Networks.Payloads;
using UnityEngine;

namespace ProjectCoin.Farms
{
    public class Field : FarmerTargetableBehaviour
    {
        [SerializeField] int fieldID = 0;

        public event Action<EFieldState> OnStateChangedEvent = null;
        public event Action<int> OnGrowUpEvent = null;

        private CropSO currentCropData = null;
        public CropSO CurrentCropData => currentCropData;

        private EFieldState currentState = EFieldState.None;
        public EFieldState CurrentState => currentState;

        public override bool TargetEnable => CurrentState != EFieldState.Growing;
        private int growth = 0;

        private void Awake()
        {
            ChangeState(EFieldState.Fallow);
        }

        public void Plant(CropSO cropData)
        {
            currentCropData = cropData;

            PlantCropRequest payload = new PlantCropRequest(currentCropData.id, fieldID);
            NetworkManager.Instance.SendWebRequest<PlantCropResponse>(payload, HandlePlantResponse);
        }

        private void HandlePlantResponse(PlantCropResponse res)
        {
            if(res.networkResult != ENetworkResult.Success)
                return;

            growth = -1;
            ChangeState(EFieldState.Dried);
            GrowUp();
            
            DateManager.Instance.OnTickCycleEvent += HandleTickCycleEvent;
        }

        public void Harvest()
        {
            DateManager.Instance.OnTickCycleEvent -= HandleTickCycleEvent;

            PlantCropRequest payload = new PlantCropRequest(currentCropData.id, fieldID);
            NetworkManager.Instance.SendWebRequest<HarvestResponse>(payload, HandleHarvestResponse);
        }

        private void HandleHarvestResponse(HarvestResponse res)
        {
            if (res.networkResult != ENetworkResult.Success)
                return;

            // currentCropData.TableRow.productCropID; 생산물 소환
            currentCropData = null;
            ChangeState(EFieldState.Fallow);
        }

        private void HandleTickCycleEvent()
        {
            if(currentState != EFieldState.Growing)
                return;

            ChangeState(EFieldState.Dried);
            GrowUp();
        }

        private void GrowUp()
        {
            growth++;
            if(growth % currentCropData.TableRow.growthRate != 0)
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
            if(prevState != currentState)
                OnStateChangedEvent?.Invoke(currentState);
        }
    }
}
