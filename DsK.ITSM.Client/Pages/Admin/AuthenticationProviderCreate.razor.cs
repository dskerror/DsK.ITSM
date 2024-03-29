﻿using DsK.ITSM.Security.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using DsK.ITSM.Security.Shared.Constants;

namespace DsK.ITSM.Client.Pages.Admin
{
    public partial class AuthenticationProviderCreate
    {
        [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; }
        private AuthenticationProviderCreateDto model = new AuthenticationProviderCreateDto();
        private bool _AccessAuthenticationProviderCreate;

        protected override async Task OnInitializedAsync()
        {
            var state = await authenticationState;
            SetPermissions(state);

            if (!_AccessAuthenticationProviderCreate)
                _navigationManager.NavigateTo("/noaccess");
        }

        private void SetPermissions(AuthenticationState state)
        {
            _AccessAuthenticationProviderCreate = securityService.HasPermission(state.User, Access.AuthenticationProvider.Create);
        }

        private async Task Create()
        {
            var result = await securityService.AuthenticationProviderCreateAsync(model);

            if (result != null)
                if (result.HasError)
                    Snackbar.Add(result.Message, Severity.Error);
                else
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    _navigationManager.NavigateTo($"/admin/AuthenticationProviderViewEdit/{result.Result.Id}");
                }
            else
                Snackbar.Add("An Unknown Error Has Occured", Severity.Error);

        }
    }
}
