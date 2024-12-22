using ProjectCoin.Farms.Helpers;

namespace ProjectCoin.Farms.AI
{
    public class StoredEggDecision : FarmerFSMDecision
    {
        public override bool MakeDecision()
        {
            Egg targetEgg = aiData.CurrentTarget as Egg;
            if(targetEgg == null)
                return false;

            Farm currentFarm = new GetBelongsFarm(targetEgg.transform).currentFarm;
            if(currentFarm == null)
                return false;

            return currentFarm.EggStorage.Contains(targetEgg);
        }
    }
}
