using H00N.DataTables;
using H00N.FSM;
using H00N.Resources;
using H00N.Resources.Pools;
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

        [Header("Test")]
        [SerializeField] CropSO cropData = null;

        public override void EnterState()
        {
            base.EnterState();

            Field currentField = aiData.CurrentTarget as Field;
            if(currentField.CurrentState != EFieldState.Empty)
            {
                brain.SetAsDefaultState();
                return;
            }

            #region Test
            currentField.SetCropData(cropData);
            #endregion

            if (currentField.CurrentCropData == null)
            {
                brain.SetAsDefaultState();
                return;
            }

            ECropType cropType = currentField.CurrentCropData.TableRow.cropType;
            switch(cropType)
            {
                case ECropType.Egg:
                    PlantDecisionEgg(currentField);
                    break;
                case ECropType.Crop:
                    PlantDecisionCrop();
                    break;
            }
        }

        private void PlantDecisionEgg(Field currentField)
        {
            Item holdItem = aiData.farmer.HoldItem;
            if (holdItem == null)
            {
                int eggCropID = currentField.CurrentCropData.TableRow.id;
                int eggItemID = DataTableManager.GetTable<EggCropTable>()[eggCropID].itemID;
                if (TryTarggetingEgg(eggItemID) == false)
                {
                    brain.SetAsDefaultState();
                    return;
                }

                brain.ChangeState(moveState);
                return;
            }
            else
            {
                aiData.farmer.ReleaseItem();
                PoolManager.Despawn(holdItem);

                brain.ChangeState(plantState);
            }
        }

        private void PlantDecisionCrop()
        {
            brain.ChangeState(plantState);
        }

        private bool TryTarggetingEgg(int eggItemID)
        {
            // 원하는 알을 잡게 해주자.
            Farm currentFarm = new GetBelongsFarm(brain.transform).currentFarm;
            if (currentFarm == null)
                return false;

            ItemSO itemData = ResourceManager.LoadResource<ItemSO>($"ItemData_{eggItemID}");
            Egg targetEgg = currentFarm.EggStorage.GetEgg(itemData);
            if(targetEgg == null)
                return false;

            aiData.PushTarget(targetEgg);
            return true;
        }
    }
}
