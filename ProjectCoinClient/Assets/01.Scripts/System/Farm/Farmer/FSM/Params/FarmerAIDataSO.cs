using H00N.FSM;
using ProjectCoin.Units;
using UnityEngine;

namespace ProjectCoin.Farms.AI
{
    [CreateAssetMenu(menuName = "SO/Farm/FarmerAIData")]
    public class FarmerAIDataSO : FSMParamSO
    {
        public FarmerStatSO farmerStat = null;
        public UnitMovement movement = null;
        public Farmer farmer = null;

        private FarmerTargetableBehaviour currenTarget = null;
        public FarmerTargetableBehaviour CurrentTarget => currenTarget;

        public void SetTarget(FarmerTargetableBehaviour target)
        {
            currenTarget = target;
            currenTarget?.SetWatcher(farmer);
        }

        public void ResetTarget()
        {
            currenTarget?.SetWatcher(null);
            currenTarget = null;
        }
    }
}
