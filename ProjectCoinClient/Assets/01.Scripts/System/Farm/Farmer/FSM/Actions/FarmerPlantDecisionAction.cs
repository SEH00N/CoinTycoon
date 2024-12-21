using H00N.DataTables;
using H00N.FSM;
using H00N.Resources;
using ProjectCoin.Datas;
using ProjectCoin.DataTables;
using ProjectCoin.Farms.Helpers;
using UnityEngine;

namespace ProjectCoin.Farms.AI
{
    public class FarmerPlantDecisionAction : FarmerFSMAction
    {
        [SerializeField] FSMState plantState = null;
        [SerializeField] FSMState moveState = null;

        public override void EnterState()
        {
            base.EnterState();

            Field currentField = aiData.CurrentTarget as Field;
            if(currentField.CurrentState != EFieldState.Empty)
            {
                brain.SetAsDefaultState();
                return;
            }

            if(currentField.CurrentCropData == null)
            {
                brain.SetAsDefaultState();
                return;
            }

            ItemTableRow itemTableRow = DataTableManager.GetTable<ItemTable>().GetRow(currentField.CurrentCropData.TableRow.productCropID);
            if(itemTableRow == null)
            {
                brain.SetAsDefaultState();
                return;
            }

            if (itemTableRow.itemType == EItemType.Egg && aiData.farmer.HoldItem == null)
            {
                if(TryTarggetingEgg(itemTableRow.id) == false)
                {
                    brain.SetAsDefaultState();
                    return;
                }

                brain.ChangeState(moveState);
                return;
            }

            // 아이템 티입이 Crop이거나 심고자 하는 알을 지니고 있는 상태
            brain.ChangeState(plantState);
        }

        private bool TryTarggetingEgg(int eggID)
        {
            // 원하는 알을 잡게 해주자.
            Farm currentFarm = new GetBelongsFarm(brain.transform).currentFarm;
            if (currentFarm == null)
                return false;

            ItemSO itemData = ResourceManager.LoadResource<ItemSO>($"ItemData_{eggID}");
            if(itemData == null)
                return false;

            Egg targetEgg = currentFarm.EggStorage.GetEgg(itemData);
            if(targetEgg == null)
                return false;

            aiData.SetTarget(targetEgg);            
            return true;
        }
    }
}
