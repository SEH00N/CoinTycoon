using H00N;
using H00N.FSM;
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
