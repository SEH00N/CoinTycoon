namespace ProjectCoin.Networks.Payloads
{
    public class PlantCropRequest : RequestPayload
    {
        public override string Route => NetworkDefine.FARM_ROUTE;

        public const string POST = "plant_crop";
        public override string Post => POST;

        public int cropID = 0;
        public int fieldID = 0;

        public PlantCropRequest(int cropID, int fieldID)
        {
            this.cropID = cropID;
            this.fieldID = fieldID;
        }
    }
}