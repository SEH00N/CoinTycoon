using H00N.Stats;
using UnityEngine;

namespace ProjectCoin.Farms.AI
{
    public class FarmerMoveAction : FarmerFSMAction
    {
        public override void UpdateState()
        {
            base.UpdateState();

            Vector3 targetPosition = aiData.currentTarget.position;
            Vector3 direction = targetPosition - brain.transform.position;
            direction.z = 0f;

            float moveSpeed = Time.deltaTime * aiData.farmerStat[EStatType.MoveSpeed];
            Vector3 moveVector = direction.normalized * moveSpeed;

            brain.transform.Translate(moveVector);
        }
    }
}