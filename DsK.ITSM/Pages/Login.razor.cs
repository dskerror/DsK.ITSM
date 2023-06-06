using Blazored.LocalStorage;
using DsK.ITSM.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Net.Http;

namespace DsK.ITSM.Pages;
public partial class Login
{
    private UserLoginDto userLoginModel = new UserLoginDto() { AuthenticationProviderId = 1};
    private bool _LoginButtonDisabled;
    private bool _passwordVisibility;
    private InputType _passwordInput = InputType.Password;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

    private async Task SubmitAsync()
    {
        _LoginButtonDisabled = true;
        //bool result = await securityService.LoginAsync(userLoginModel);
        //if (result)
        //{
        //    _navigationManager.NavigateTo("/");
        //    Snackbar.Add("Login Successful", Severity.Success);
        //}
        //else
        //    Snackbar.Add("Username and/or Password incorrect", Severity.Error);

        _LoginButtonDisabled = false;
    }

    private void LoginAsync()
    {
        APIResult<TokenModel> result = new APIResult<TokenModel>();
        var user = await AuthenticateUser(model);

        if (user == null)
        {
            result.HasError = true;
            return result;
        }

        var token = await GenerateAuthenticationToken(user);

        db.UserTokens.Add(new UserToken()
        {
            UserId = user.Id,
            RefreshToken = token.RefreshToken,
            TokenRefreshedDateTime = DateTime.Now,
            TokenCreatedDateTime = DateTime.Now
        });

        await db.SaveChangesAsync();

        result.Result = token;
        return result;

        var tokenModel = await SecurityService.UserLogin(model);

        if (tokenModel == null)
            return NotFound();

        return Ok(tokenModel);

        await _localStorageService.SetItemAsync("token", result.Result.Token);
        await _localStorageService.SetItemAsync("refreshToken", result.Result.RefreshToken);
        (_authenticationStateProvider as CustomAuthenticationStateProvider).Notify();
        return true;
    }
    void TogglePasswordVisibility()
    {
        if (_passwordVisibility)
        {
            _passwordVisibility = false;
            _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
            _passwordInput = InputType.Password;
        }
        else
        {
            _passwordVisibility = true;
            _passwordInputIcon = Icons.Material.Filled.Visibility;
            _passwordInput = InputType.Text;
        }
    }
}