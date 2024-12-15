using UnityEngine;
using UnityEngine.Rendering;

namespace ProjectCoin.Farms
{
    public class FarmerAnimator : MonoBehaviour
    {
        private Animator animator = null;

        private readonly int IS_IDLE_HASH = Animator.StringToHash("is_idle");
        private readonly int IS_MOVE_HASH = Animator.StringToHash("is_move");
        private readonly int IS_WATER_HASH = Animator.StringToHash("is_water");
        private readonly int IS_PLOW_HASH = Animator.StringToHash("is_plow");
        private readonly int IS_HARVEST_HASH = Animator.StringToHash("is_harvest");
        private readonly int IS_LIFT_HASH = Animator.StringToHash("is_lift");
        private readonly int IS_LOAD_HASH = Animator.StringToHash("is_load");

        private int currentHash = 0;

        private void Awake()
        {
            currentHash = IS_IDLE_HASH;
            animator = GetComponent<Animator>();
        }
    
        public void SetIdle() => ChangeState(IS_IDLE_HASH);
        public void SetMove() => ChangeState(IS_MOVE_HASH);
        public void SetWater() => ChangeState(IS_WATER_HASH);
        public void SetPlow() => ChangeState(IS_PLOW_HASH);
        public void SetHarvest() => ChangeState(IS_HARVEST_HASH);
        public void SetLift() => ChangeState(IS_LIFT_HASH);
        public void SetLoad() => ChangeState(IS_LOAD_HASH);

        private void ChangeState(int hash)
        {
            if(currentHash == hash)
                return;

            animator.SetBool(currentHash, false);
            currentHash = hash;
            animator.SetBool(currentHash, true);
        }
    }
}
