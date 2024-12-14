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

        private CropsSO currentCropsData = null;
        public CropsSO CurrentCropsData => currentCropsData;

        private EFieldState currentState = EFieldState.None;
        public EFieldState CurrentState => currentState;

        private int growth = 0;

        #if UNITY_EDITOR
        [Space(10f)]
        [SerializeField] CropsSO testData = null;

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
            currentState = EFieldState.Empty;
        }

        public void Plant(CropsSO cropsData)
        {
            currentCropsData = cropsData;
            currentState = EFieldState.None;

            PlantCropsRequest payload = new PlantCropsRequest(currentCropsData.id, fieldID);
            NetworkManager.Instance.SendWebRequest<PlantCropsResponse>(payload, HandlePlantCropsResponse);
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

            // currentCropsData.TableRow.productCropID; 생산물 소환
            currentCropsData = null;
        }

        private void HandlePlantCropsResponse(PlantCropsResponse res)
        {
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
            if(growth % currentCropsData.TableRow.growthRate != 0)
                return;

            int currentStep = growth / currentCropsData.TableRow.growthRate;
            OnGrowUpEvent?.Invoke(currentStep);

            if (currentStep >= currentCropsData.TableRow.growthStep - 1)
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
