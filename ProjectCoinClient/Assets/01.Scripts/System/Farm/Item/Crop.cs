
namespace ProjectCoin.Farms
{
    public class Crop : Item
    {
        public override FarmerTargetableBehaviour DeliveryTarget => FindObjectOfType<CropStorage>();
    }
}