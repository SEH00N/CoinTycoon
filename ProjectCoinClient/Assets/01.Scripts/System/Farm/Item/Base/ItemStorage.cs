using ProjectCoin.Farms;
using UnityEngine;

namespace ProjectCoin.Farms
{
    public class ItemStorage : FarmerTargetableBehaviour
    {
        [SerializeField] Transform entranceTransform = null;
        public override Vector3 TargetPosition => entranceTransform.position;
    }
}