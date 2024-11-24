using Microsoft.AspNetCore.Mvc;
using ProjectCoin.Networks.Payloads;
using ProjectCoin.Datas;

namespace ProjectCoinServer.Controllers
{
    [ApiController]
    [Route(RankingController.ROUTE)]
    public class RankingController : ControllerBase
    {
        public const string ROUTE = "ranking";

        [HttpPost(RankingListRequest.POST)]
        public async Task<ActionResult<RankingListResponse>> RankingListRequestPost([FromBody]RankingListRequest req)
        {
            List<RankingData> rankingList = new List<RankingData>();
            // make raking list from redis

            RankingListResponse response = new RankingListResponse() {
                RankingList = rankingList
            };

            return response;
        }
    }
}
