using Cysharp.Threading.Tasks;
using H00N.Resources;
using ProjectCoin.Farms;

namespace ProjectCoin.Items
{
    public class Item : FarmerTargetableBehaviour
    {
        private ItemSO itemData = null;
        public ItemSO ItemData => itemData;

        public virtual async UniTask Initialize(int index)
        {
            itemData = await ResourceManager.LoadResourceAsync<ItemSO>($"ItemData_{index}");
        }
    }
}