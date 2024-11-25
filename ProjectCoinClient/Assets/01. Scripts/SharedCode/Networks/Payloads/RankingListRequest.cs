namespace ProjectCoin.Networks.Payloads
{
    public class RankingListRequest : RequestPayload
    {
        public const string POST = "ranking_list";

        public int Index { get; set; }
        public int Count { get; set; }
    }
}
