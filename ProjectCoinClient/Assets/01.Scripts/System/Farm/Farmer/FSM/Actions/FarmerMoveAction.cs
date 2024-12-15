using H00N.Extensions;
using UnityEngine;

namespace ProjectCoin.Farms.AI
{
    public class FarmerMoveAction : FarmerFSMAction
    {
        public override void UpdateState()
        {
            base.UpdateState();

            if(aiData.currentTarget == null)
                return;

            Vector3 targetPosition = aiData.currentTarget.position;
            aiData.movement.SetDestination(targetPosition.PlaneVector());
        }
    }
}