namespace DsK.ITSM.API.HttpClients;

public class AuthorizarionServerAPIHttpClient
{
    public AuthorizarionServerAPIHttpClient(HttpClient client)
    {
        Client = client;
    }
    public HttpClient Client { get; }
}