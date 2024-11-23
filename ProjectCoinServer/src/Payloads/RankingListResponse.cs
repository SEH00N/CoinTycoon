using ProjectCoinServer.System;

namespace ProjectCoinServer.Payloads;

public class RankingListResponse : ResponsePayload
{
    public List<RankingData> RankingList { get; set; }
}