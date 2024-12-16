namespace ProjectCoin.Farms.AI
{
    public class TargetFoundedDecision : FarmerFSMDecision
    {
        public override bool MakeDecision()
        {
            return aiData.currentTarget != null;
        }
    }
}
