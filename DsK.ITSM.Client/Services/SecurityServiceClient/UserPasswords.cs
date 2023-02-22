using DsK.ITSM.Security.Shared;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using DsK.ITSM.Client.Services.Requests;

namespace DsK.ITSM.Client.Services;

public partial class SecurityServiceClient
{  
    public async Task<APIResult<string>> UserPasswordCreateAsync(UserCreateLocalPasswordDto model)
    {
        await PrepareBearerToken();
        var response = await _httpClient.PostAsJsonAsync(Routes.UserPasswordEndpoints.Post, model);
        if (!response.IsSuccessStatusCode)        
            return null;
        
        var result = await response.Content.ReadFromJsonAsync<APIResult<string>>();
        return result;
    }   
}
