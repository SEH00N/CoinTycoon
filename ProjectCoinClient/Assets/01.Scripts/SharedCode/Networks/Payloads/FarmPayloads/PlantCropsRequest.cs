namespace ProjectCoin.Networks.Payloads
{
    public class PlantCropsRequest : RequestPayload
    {
        public override string Route => NetworkDefine.FARM_ROUTE;

        public const string POST = "plant_crops";
        public override string Post => POST;

        public int cropID = 0;
        public int fieldID = 0;

        public PlantCropsRequest(int cropID, int fieldID)
        {
            this.cropID = cropID;
            this.fieldID = fieldID;
        }
    }
}