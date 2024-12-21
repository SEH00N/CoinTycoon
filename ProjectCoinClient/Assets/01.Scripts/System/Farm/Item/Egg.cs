namespace ProjectCoin.Farms
{
    public class Egg : Item
    {
        public override FarmerTargetableBehaviour DeliveryTarget => FindObjectOfType<EggStorage>();

        // private FarmerTargetableBehaviour GetDeliveryTarget()
        // {
            
        // }
    }
}