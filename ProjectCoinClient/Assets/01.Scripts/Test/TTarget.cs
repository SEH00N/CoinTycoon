using H00N.FSM;
using ProjectCoin.Farms.AI;
using UnityEngine;

namespace ProjectCoin.Tests
{
    public class TTarget : FarmerFSMAction
    {
        [SerializeField] Transform testTarget = null;

        public override void Init(FSMBrain brain, FSMState state)
        {
            base.Init(brain, state);
            brain.GetFSMParam<FarmerAIDataSO>().currentTarget = testTarget;
        }
    }
}
