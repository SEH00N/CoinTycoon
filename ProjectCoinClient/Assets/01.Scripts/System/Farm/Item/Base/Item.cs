using Cysharp.Threading.Tasks;
using H00N.Resources;
using ProjectCoin.Datas;
using UnityEngine;

namespace ProjectCoin.Farms
{
    public abstract class Item : FarmerTargetableBehaviour
    {
        private ItemSO itemData = null;
        public ItemSO ItemData => itemData;

        public EItemType ItemType => ItemData.TableRow.itemType;

        public virtual async UniTask Initialize(int index)
        {
            itemData = await ResourceManager.LoadResourceAsync<ItemSO>($"ItemData_{index}");
        }
    }
}