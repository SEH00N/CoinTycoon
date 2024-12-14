using Microsoft.AspNetCore.Mvc;
using ProjectCoin.Networks.Payloads;

namespace ProjectCoin.Networks.Controllers
{
    [ApiController]
    [Route(NetworkDefine.FARM_ROUTE)]
    public class FarmController : ControllerBase
    {
        [HttpPost(PlantCropRequest.POST)]
        public async Task<ActionResult<PlantCropResponse>> PlantCropRequestPost([FromBody]PlantCropRequest req)
        {
            // db write

            PlantCropResponse response = new PlantCropResponse() {
                networkResult = ENetworkResult.Success,
                cropID = req.cropID
            };

            return response;
        }

        [HttpPost(HarvestRequest.POST)]
        public async Task<ActionResult<HarvestResponse>> HarvestRequestPost([FromBody]HarvestRequest req)
        {
            // db write

            HarvestResponse response = new HarvestResponse() {
                networkResult = ENetworkResult.Success,
                productCropID = 0
            };

            return response;
        }
    }
}
