using DsK.ITSM.Security.Shared;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using DsK.ITSM.Client.Services.Requests;

namespace DsK.ITSM.Client.Services;

public partial class SecurityServiceClient
{
    public async Task<APIResult<List<UserRoleGridDto>>> UserRolesGetAsync(int UserId)
    {
        await PrepareBearerToken();
        var response = await _httpClient.GetAsync(Routes.UserRolesEndpoints.Get(UserId));
        if (!response.IsSuccessStatusCode)
            return null;

        var responseAsString = await response.Content.ReadAsStringAsync();

        try
        {
            var responseObject = JsonSerializer.Deserialize<APIResult<List<UserRoleGridDto>>>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true
            });

            return responseObject;
        }
        catch (Exception ex)
        {

            Console.Write(ex.Message);
            return null;
        }
    }

    public async Task<APIResult<string>> UserRoleChangeAsync(int userId, int roleId, bool roleEnabled)
    {
        await PrepareBearerToken();
        var model = new UserRoleChangeDto()
        {
            UserId = userId,
            RoleId = roleId,
            RoleEnabled = roleEnabled
        };
        var response = await _httpClient.PostAsJsonAsync(Routes.UserRolesEndpoints.Post, model);
        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<APIResult<string>>();
        return result;
    }
}
