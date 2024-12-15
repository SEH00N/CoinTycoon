using System;
using ProjectCoin.Datas;
using ProjectCoin.Networks;
using ProjectCoin.Networks.Payloads;
using UnityEngine;

namespace ProjectCoin.Farms
{
    public class Field : MonoBehaviour
    {
        [SerializeField] int fieldID = 0;

        public event Action<EFieldState> OnStateChangedEvent = null;
        public event Action<int> OnGrowUpEvent = null;

        private CropSO currentCropData = null;
        public CropSO CurrentCropData => currentCropData;

        private EFieldState currentState = EFieldState.None;
        public EFieldState CurrentState => currentState;

        private int growth = 0;

        #if UNITY_EDITOR
        [Space(10f)]
        [SerializeField] CropSO testData = null;

        [ContextMenu("TestPlant")]
        public void TestPlant()
        {
            Plant(testData);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                Water();
        }
        #endif

        private void Awake()
        {
            ChangeState(EFieldState.Fallow);
        }

        public void Plow()
        {
            if(currentState != EFieldState.Fallow)
                return;

            ChangeState(EFieldState.Empty);
        }

        public void Plant(CropSO cropData)
        {
            if(currentState != EFieldState.Empty)
                return;

            currentCropData = cropData;

            PlantCropRequest payload = new PlantCropRequest(currentCropData.id, fieldID);
            NetworkManager.Instance.SendWebRequest<PlantCropResponse>(payload, HandlePlantCropResponse);
        }
        
        public void Water()
        {
            if(currentState != EFieldState.Dried)
                return;

            ChangeState(EFieldState.Growing);
        }

        public void Harvest()
        {
            if(currentState != EFieldState.Fruition)
                return;

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

        private void HandlePlantCropResponse(PlantCropResponse res)
        {
            if(res.networkResult != ENetworkResult.Success)
                return;

            growth = -1;
            ChangeState(EFieldState.Dried);
            GrowUp();
            
            DateManager.Instance.OnTickCycleEvent += HandleTickCycleEvent;
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

        private void ChangeState(EFieldState targetState)
        {
            EFieldState prevState = currentState;
            currentState = targetState;
            if(prevState != currentState)
                OnStateChangedEvent?.Invoke(currentState);
        }
    }
}
