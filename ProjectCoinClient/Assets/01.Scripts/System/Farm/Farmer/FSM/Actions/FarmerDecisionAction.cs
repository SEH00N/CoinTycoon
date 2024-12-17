using System.Collections.Generic;
using System.Linq;
using H00N.Extensions;
using ProjectCoin.Datas;
using UnityEngine;

namespace ProjectCoin.Farms.AI
{
    public class FarmerDecisionAction : FarmerFSMAction
    {
        // Idle | 대기시간 공식 : Random(0, (200 - iq) / 40 * 2f) + 1
        // Field | 밭 선택 규칙 : 거리순 정렬 -> count - (count * (iq - 40) / (200 - 40))회 셔플
        // iq 는 40 ~ 200 값

        private bool fieldDecided = false;
        private float idleTimer = 0f;

        public override void EnterState()
        {
            base.EnterState();

            fieldDecided = false;
            SetIdle();
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if(fieldDecided)
                return;

            idleTimer -= Time.deltaTime;
            if(idleTimer > 0f)
                return;

            DecideAction();
        }

        private void DecideAction()
        {
            // 1/3 확률로 Idle, Field 정해짐
            int randomValue = Random.Range(0, 3);
            if(randomValue > 1)
                SetField();
            else
                SetIdle();
        }

        private void SetIdle()
        {
            float range = (DEFINE.IQ_STAT_MAX - aiData.farmerStat[EFarmerStatType.IQ]) / DEFINE.IQ_STAT_MIN * 2f;
            float idleDuration = Random.Range(0f, range) + 1f;
            idleTimer = idleDuration;
        }

        private void SetField()
        {
            // 나중에 바꿔야 함
            List<Transform> fields = FindObjectsOfType<FarmerTargetableBehaviour>()
                .Where(i => i.TargetEnable && !i.IsWatched)
                .Select(i => i.transform).ToList();

            if(fields.Count <= 0)
            {
                SetIdle();
                return;
            }

            int fieldCount = fields.Count;
            float theta = (aiData.farmerStat[EFarmerStatType.IQ] - DEFINE.IQ_STAT_MIN) / (DEFINE.IQ_STAT_MAX - DEFINE.IQ_STAT_MIN);
            int shuffleCount = fieldCount - Mathf.RoundToInt(fieldCount * theta);
            
            fields.Sort(transform.DistanceCompare);
            fields.PickShuffle(shuffleCount);
            aiData.currentTarget = fields[0].GetComponent<FarmerTargetableBehaviour>();

            fieldDecided = true;
        }
    }
}
