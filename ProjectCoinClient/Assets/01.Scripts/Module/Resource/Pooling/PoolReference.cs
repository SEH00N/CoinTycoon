using System.Collections.Generic;
using UnityEngine;

namespace H00N.Resources.Pools
{
    public class PoolReference : MonoBehaviour
    {
        private List<PoolableBehaviour> poolableBehaviours = null;
        
        private ResourceHandle handle = null;
        public ResourceHandle Handle => handle;
        
        protected virtual void Awake()
        {
            poolableBehaviours = new List<PoolableBehaviour>();
            GetComponents<PoolableBehaviour>(poolableBehaviours);
        }

        internal void Initialize(ResourceHandle handle)
        {
            this.handle = handle;
        }

        public void Spawn()
        {
            poolableBehaviours.ForEach(i => i?.OnSpawned());
            SpawnInternal();
        }

        protected virtual void SpawnInternal() { }

        public void Despawn()
        {
            poolableBehaviours.ForEach(i => i?.OnDespawn());
            DespawnInternal();
        }

        protected virtual void DespawnInternal() { }
    }
}