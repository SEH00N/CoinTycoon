using H00N.FSM;
using ProjectCoin.Items;
using UnityEngine;

namespace ProjectCoin.Farms.AI
{
    public class FarmerLiftAction : FarmerAnimationAction
    {
        [SerializeField] Transform grabParent = null;
        [SerializeField] FSMState moveState = null;
        private Item currentItem = null;

        public override void EnterState()
        {
            base.EnterState();

            currentItem = aiData.CurrentTarget as Item;
            if(currentItem == null)
                brain.SetAsDefaultState();
        }

        protected override void OnHandleAnimationTrigger()
        {
            base.OnHandleAnimationTrigger();

            currentItem.transform.SetParent(grabParent);
            aiData.ResetTarget();
        }

        protected override void OnHandleAnimationEnd()
        {
            base.OnHandleAnimationEnd();

            // 이건 좀 더 고민해봐야 할 듯
            // 바닥에 떨어진 작물을 든 경우
            // 바닥에 떨어진 알을 든 경우
            // 둥지에 있던 알을 든 경우

            // 이런식으로 하드코딩 하기에는 컨텐츠가 늘어날 수록 좋지 않아보임.
            // 객체지향화가 필요하다.
            // 아마 IGrabbale, IStorable 이런 인터페이스 만들어서
            // FarmerTargetableBehaviour TargetStorage 반환하게 하지 않을까 싶음
            // aiData.SetTarget(?);

            brain.ChangeState(moveState);
        }
    }
}
