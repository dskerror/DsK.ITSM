using DsK.ITSM.Security.Shared;
using Newtonsoft.Json;

namespace DsK.ITSM.Client.Services;

public partial class SecurityServiceClient
{  
    public async Task<APIResult<List<CategoryDto>>> CategoriesGetAsync()
    {
        await PrepareBearerToken();
        var response = await _httpClient.GetAsync(Routes.CategoriesEndpoints.Get());
        
        if (!response.IsSuccessStatusCode)        
            return null;

        var responseAsString = await response.Content.ReadAsStringAsync();

        try
        {
            var responseObject = JsonConvert.DeserializeObject<APIResult<List<CategoryDto>>>(responseAsString);
            return responseObject;
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
            return null;
        }
    }
}