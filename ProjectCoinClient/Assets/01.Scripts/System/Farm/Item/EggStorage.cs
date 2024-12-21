using System.Collections.Generic;

namespace ProjectCoin.Farms
{
    public class EggStorage : ItemStorage
    {
        private List<Egg> eggList = null;

        protected override void Awake()
        {
            eggList = null;
        }
        
        public Egg GetEgg(ItemSO itemData)
        {
            Egg egg = eggList.Find(i => i.ItemData == itemData);
            return egg;
        }

        public override bool ConsumeItem(ItemSO itemData)
        {
            bool result = base.ConsumeItem(itemData);

            int index = eggList.FindIndex(i => i.ItemData ==itemData);
            if(index != -1)
                eggList.RemoveAt(index);

            return result;
        }

        public override void StoreItem(Item item)
        {
            base.StoreItem(item);
            eggList.Add(item as Egg);
        }
    }
}