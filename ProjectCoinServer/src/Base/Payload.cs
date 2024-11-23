namespace ProjectCoinServer;

public class Payload
{
}

public class RequestPayload : Payload
{
    public string UserID { get; set; }
}

public class ResponsePayload : Payload
{
    public ENetworkResult NetworkResult { get; set; }
}