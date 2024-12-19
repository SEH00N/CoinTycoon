using UnityEngine;

namespace ProjectCoin.Farms
{
    public class ItemStorage : FarmerTargetableBehaviour
    {
        [SerializeField] Transform entranceTransform = null;
        public override Vector3 TargetPosition => entranceTransform.position;

        #if UNITY_EDITOR
        protected override void DrawGizmos()
        {
            if(entranceTransform == null)
                return;

            base.DrawGizmos();
        }
        #endif
    }
}