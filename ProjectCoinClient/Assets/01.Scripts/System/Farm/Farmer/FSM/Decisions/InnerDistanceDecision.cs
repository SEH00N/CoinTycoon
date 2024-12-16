using UnityEngine;

namespace ProjectCoin.Farms.AI
{
    public class InnerDistanceDecision : FarmerFSMDecision
    {
        [SerializeField] float distanceThreshold = 0.5f;

        public override bool MakeDecision()
        {
            if(aiData.currentTarget == null)
                return false;

            Vector3 directionVector = aiData.currentTarget.position - brain.transform.position;
            bool condition = directionVector.sqrMagnitude < (distanceThreshold * distanceThreshold);

            return condition;
        }
    }
}