﻿using DsK.ITSM.Security.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using DsK.ITSM.Security.Shared.Constants;

namespace DsK.ITSM.Client.Pages.Admin
{
    public partial class RoleCreate
    {
        [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; }
        private RoleCreateDto model = new RoleCreateDto();
        private bool _AccessRolesCreate;

        protected override async Task OnInitializedAsync()
        {
            var state = await authenticationState;
            SetPermissions(state);

            if (!_AccessRolesCreate)
                _navigationManager.NavigateTo("/noaccess");
        }

        private void SetPermissions(AuthenticationState state)
        {
            _AccessRolesCreate = securityService.HasPermission(state.User, Access.Roles.Create);
        }

        private async Task Create()
        {
            var result = await securityService.RoleCreateAsync(model);

            if (result != null)
                if (result.HasError)
                    Snackbar.Add(result.Message, Severity.Error);
                else
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    _navigationManager.NavigateTo($"/admin/roleviewedit/{result.Result.Id}");
                }
            else
                Snackbar.Add("An Unknown Error Has Occured", Severity.Error);

        }
    }
}
