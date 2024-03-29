﻿using DsK.ITSM.Security.Shared;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using DsK.ITSM.Client.Services.Requests;
using Newtonsoft.Json;

namespace DsK.ITSM.Client.Services;

public partial class SecurityServiceClient
{  
    public async Task<APIResult<UserDto>> UserCreateAsync(UserCreateDto model)
    {
        await PrepareBearerToken();
        var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.Post, model);
        if (!response.IsSuccessStatusCode)        
            return null;
        
        var result = await response.Content.ReadFromJsonAsync<APIResult<UserDto>>();
        return result;
    }
    public async Task<APIResult<UserDto>> UserEditAsync(UserDto model)
    {
        await PrepareBearerToken();
        var response = await _httpClient.PutAsJsonAsync(Routes.UserEndpoints.Put, model);
        if (!response.IsSuccessStatusCode)        
            return null;
        
        var result = await response.Content.ReadFromJsonAsync<APIResult<UserDto>>();
        return result;
    }
    public async Task<APIResult<List<UserDto>>> UsersGetAsync(PagedRequest request)
    {
        await PrepareBearerToken();
        var response = await _httpClient.GetAsync(Routes.UserEndpoints.Get(request.Id, request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
        
        if (!response.IsSuccessStatusCode)        
            return null;

        var responseAsString = await response.Content.ReadAsStringAsync();

        try
        {
            var responseObject = JsonConvert.DeserializeObject<APIResult<List<UserDto>>>(responseAsString);
            //var responseObject = JsonSerializer.Deserialize<APIResult<List<UserDto>>>(responseAsString, new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true,
            //    ReferenceHandler = ReferenceHandler.Preserve,
            //    IncludeFields = true
            //});

            return responseObject;
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
            return null;
        }
    }
    public async Task<APIResult<UserDto>> UserGetAsync(int id)
    {
        var result = await UsersGetAsync(new PagedRequest() { Id = id});
        var newResult = new APIResult<UserDto>
        {
            Exception = result.Exception,
            HasError = result.HasError,
            Message = result.Message,
            Result = result.Result.FirstOrDefault()
        };

        return newResult;
    }
}
