using UnityEngine;

namespace ProjectCoin.Farms
{
    public class FarmerTargetableBehaviour : MonoBehaviour
    {
        private Farmer watcher = null;
        // public Farmer Watcher => watcher;

        public virtual bool TargetEnable => true;
        public bool IsWatched => watcher != false;

        public void SetWatcher(Farmer watcher)
        {
            this.watcher = watcher;
        }
    }
}