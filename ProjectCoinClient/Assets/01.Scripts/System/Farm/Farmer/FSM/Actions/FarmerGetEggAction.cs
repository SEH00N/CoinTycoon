using H00N.FSM;
using ProjectCoin.Farms.Helpers;
using UnityEngine;

namespace ProjectCoin.Farms.AI
{
    public class FarmerGetEggAction : FarmerFSMAction
    {
        [SerializeField] FSMState liftState = null;

        public override void EnterState()
        {
            base.EnterState();

            Egg currentEgg = aiData.CurrentTarget as Egg;
            if(currentEgg == null)
            {
                brain.SetAsDefaultState();
                return;
            }

            Farm currentFarm = new GetBelongsFarm(brain.transform).currentFarm;
            if(currentFarm == null)
            {
                brain.SetAsDefaultState();
                return;
            }

            if(currentFarm.EggStorage.ConsumeItem(currentEgg.ItemData) == false)
                return;

            brain.ChangeState(liftState);
        }
    }
}
