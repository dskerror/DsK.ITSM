using DsK.ITSM.Security.Shared;
using DsK.ITSM.Client.Services.Requests;
using Newtonsoft.Json;

namespace DsK.ITSM.Client.Services;
public partial class SecurityServiceClient
{     
    public async Task<APIResult<List<RequestDto>>> RequestsGetAsync(PagedRequest request)
    {
        await PrepareBearerToken();
        var response = await _httpClient.GetAsync(Routes.RequestsEndpoints.Get(request.Id, request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
        
        if (!response.IsSuccessStatusCode)        
            return null;

        var responseAsString = await response.Content.ReadAsStringAsync();

        try
        {
            var responseObject = JsonConvert.DeserializeObject<APIResult<List<RequestDto>>>(responseAsString);
            return responseObject;
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
            return null;
        }
    }

    public async Task<APIResult<List<UserDto>>> RequestedByUserListGetAsync()
    {
        await PrepareBearerToken();
        var response = await _httpClient.GetAsync(Routes.RequestsEndpoints.RequestedByUserListGet());

        if (!response.IsSuccessStatusCode)
            return null;

        var responseAsString = await response.Content.ReadAsStringAsync();

        try
        {
            var responseObject = JsonConvert.DeserializeObject<APIResult<List<UserDto>>>(responseAsString);
            return responseObject;
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
            return null;
        }
    }
}