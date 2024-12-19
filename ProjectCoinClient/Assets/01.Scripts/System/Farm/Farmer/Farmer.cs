using H00N.FSM;
using H00N.Resources;
using H00N.Resources.Pools;
using ProjectCoin.Farms.AI;
using ProjectCoin.Units;

namespace ProjectCoin.Farms
{
    public class Farmer : PoolReference
    {
        private FarmerStatSO statData = null;

        private FSMBrain fsmBrain = null;
        private UnitMovement unitMovement = null;

        protected override void Awake()
        {
            base.Awake();

            unitMovement = GetComponent<UnitMovement>();
            fsmBrain = GetComponent<FSMBrain>();
        }

        public async void InitializeAsync(int id)
        {
            statData = await ResourceManager.LoadResourceAsync<FarmerStatSO>($"FarmerStat_{id}");
            unitMovement.SetMaxSpeed(statData[EFarmerStatType.MoveSpeed]);
            unitMovement.SetDestination(transform.position);

            fsmBrain.Initialize();
            FarmerAIDataSO aiData = fsmBrain.GetFSMParam<FarmerAIDataSO>();
            aiData.farmerStat = statData;
            aiData.farmer = this;
            aiData.movement = unitMovement;
            aiData.ResetTarget();
        }
    }
}
