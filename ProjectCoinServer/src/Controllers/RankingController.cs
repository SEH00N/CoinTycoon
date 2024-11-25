using Microsoft.AspNetCore.Mvc;
using ProjectCoinServer.Payloads;
using ProjectCoinServer.System;

namespace ProjectCoinServer.Controllers;

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

    [HttpGet("TestGet")]
    public async Task<ActionResult<int>> TestGet()
    {
        return 10;
    }
}