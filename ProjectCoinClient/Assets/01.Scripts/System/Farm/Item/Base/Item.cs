using Cysharp.Threading.Tasks;
using H00N.Resources;
using ProjectCoin.Datas;

namespace ProjectCoin.Farms
{
    public abstract class Item : FarmerTargetableBehaviour
    {
        private ItemSO itemData = null;
        public ItemSO ItemData => itemData;

        public EItemType ItemType => ItemData.TableRow.itemType;
        public FarmerTargetableBehaviour DeliveryTarget => GetDeliveryTarget();

        private Farmer holder = null;
        public Farmer Holder => holder;

        public virtual async UniTask Initialize(int index)
        {
            itemData = await ResourceManager.LoadResourceAsync<ItemSO>($"ItemData_{index}");
        }

        public void SetHolder(Farmer farmer)
        {
            holder = farmer;
        }

        protected abstract FarmerTargetableBehaviour GetDeliveryTarget();
    }
}