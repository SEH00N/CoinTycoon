using System.Collections.Generic;
using H00N.Resources.Pools;
using UnityEngine;

namespace ProjectCoin.Farms
{
    public class ItemStorage : FarmerTargetableBehaviour
    {
        [SerializeField] Transform entranceTransform = null;
        public override Vector3 TargetPosition => entranceTransform.position;

        public override bool TargetEnable => Watcher != null;

        private Dictionary<ItemSO, int> storage = null;

        protected override void Awake()
        {
            base.Awake();
            storage = new Dictionary<ItemSO, int>();
        }

        public bool ConsumeItem(ItemSO itemData)
        {
            if (storage.TryGetValue(itemData, out int count) == false)
                return false;

            if(count <= 0)
                return false;

            storage[itemData]--;
            return true;
        }

        public void StoreItem(Item item)
        {
            StoreItem(item.ItemData);
            PoolManager.Despawn(item);
        }

        public void StoreItem(ItemSO itemData)
        {
            if (storage.ContainsKey(itemData) == false)
                storage.Add(itemData, 0);

            storage[itemData]++;
        }

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