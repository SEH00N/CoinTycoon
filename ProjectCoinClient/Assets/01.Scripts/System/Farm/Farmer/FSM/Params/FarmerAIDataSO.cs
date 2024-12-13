using H00N.FSM;
using H00N.Stats;
using UnityEngine;

namespace ProjectCoin.Farms.AI
{
    [CreateAssetMenu(menuName = "SO/Farm/FarmerAIData")]
    public class FarmerAIDataSO : FSMParamSO
    {
        public StatSO farmerStat = null;
        public Transform currentTarget = null;
    }
}
