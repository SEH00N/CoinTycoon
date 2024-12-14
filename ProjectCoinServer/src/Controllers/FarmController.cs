using Microsoft.AspNetCore.Mvc;
using ProjectCoin.Networks.Payloads;

namespace ProjectCoin.Networks.Controllers
{
    [ApiController]
    [Route(NetworkDefine.FARM_ROUTE)]
    public class FarmController : ControllerBase
    {
        [HttpPost(PlantCropsRequest.POST)]
        public async Task<ActionResult<PlantCropsResponse>> PlantCropsRequestPost([FromBody]PlantCropsRequest req)
        {
            // db write

            PlantCropsResponse response = new PlantCropsResponse() {
                networkResult = ENetworkResult.Success,
                cropID = req.cropID
            };

            return response;
        }
    }
}
