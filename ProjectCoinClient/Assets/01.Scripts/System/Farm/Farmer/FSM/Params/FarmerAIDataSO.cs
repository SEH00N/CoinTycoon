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
        public FarmerTargetableBehaviour currentTarget = null;
    }
}
