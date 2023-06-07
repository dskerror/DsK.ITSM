using Blazored.LocalStorage;
using DsK.ITSM.Dto;
using DsK.ITSM.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace DsK.ITSM.Pages;
public partial class Login
{
    private UserLoginDto userLoginModel = new UserLoginDto() { AuthenticationProviderId = 1 };
    private bool _LoginButtonDisabled;
    private bool _passwordVisibility;
    private InputType _passwordInput = InputType.Password;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

    [Inject]
    protected ILocalStorageService _localStorageService { get; set; }
    [Inject]
    protected AuthenticationStateProvider _authenticationStateProvider { get; set; }
    [Inject]
    protected SecurityService _securityService { get; set; }
    //public Login(ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider, SecurityService securityService)
    //{        
    //    _localStorageService = localStorageService;        
    //    _authenticationStateProvider = authenticationStateProvider;
    //    _securityService = securityService;
    //}

    private async Task SubmitAsync()
    {

        _LoginButtonDisabled = true;
        userLoginModel.AuthenticationProviderId = 2;
        var serviceResponse = await _securityService.UserLogin(userLoginModel);
        if (serviceResponse == null || serviceResponse.HasError)
        {
            await _localStorageService.SetItemAsync("token", serviceResponse.Result.Token);
            await _localStorageService.SetItemAsync("refreshToken", serviceResponse.Result.RefreshToken);
            (_authenticationStateProvider as CustomAuthenticationStateProvider).Notify();
            _navigationManager.NavigateTo("/");
            Snackbar.Add("Login Successful", Severity.Success);
        }
        else
            Snackbar.Add("Username and/or Password incorrect", Severity.Error);

        _LoginButtonDisabled = false;
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