using ProjectCoin.Datas;
using UnityEngine;

namespace ProjectCoin.Farms.AI
{
    public class FarmerPlantAction : FarmerAnimationAction
    {
        [Header("Test")]
        [SerializeField] CropSO cropData = null;
        private Field currentField = null;

        public override void EnterState()
        {
            base.EnterState();

            currentField = aiData.currentTarget as Field;
            if(currentField.CurrentState != EFieldState.Empty)
                brain.SetAsDefaultState();
        }

        protected override void OnHandleAnimationTrigger()
        {
            base.OnHandleAnimationTrigger();
            if(currentField.CurrentState == EFieldState.Empty)
                currentField.Plant(cropData);
        }

        protected override void OnHandleAnimationEnd()
        {
            base.OnHandleAnimationEnd();
            brain.SetAsDefaultState();
        }
    }
}
