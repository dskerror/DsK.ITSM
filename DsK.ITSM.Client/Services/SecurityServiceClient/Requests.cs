using DsK.ITSM.Security.Shared;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using DsK.ITSM.Client.Services.Requests;
using System.Text.Encodings.Web;
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
}
