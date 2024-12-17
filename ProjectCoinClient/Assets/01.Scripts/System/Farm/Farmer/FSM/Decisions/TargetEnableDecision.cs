namespace ProjectCoin.Farms.AI
{
    public class TargetEnableDecision : FarmerFSMDecision
    {
        public override bool MakeDecision()
        {
            if(aiData.currentTarget == null)
                return false;

            if(aiData.currentTarget.TargetEnable == false)
                return false;

            if(aiData.currentTarget.IsWatched)
                return false;

            return true;
        }
    }
}
