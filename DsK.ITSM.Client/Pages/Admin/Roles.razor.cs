﻿using DsK.ITSM.Client.Services.Requests;
using DsK.ITSM.Security.Shared;
using DsK.ITSM.Security.Shared.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace DsK.ITSM.Client.Pages.Admin
{
    public partial class Roles
    {
        [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; }
        private IEnumerable<RoleDto> _pagedData;
        private MudTable<RoleDto> _table;
        private bool _loaded;
        private int _totalItems;
        private int _currentPage;
        private string _searchString = "";
        private bool _AccessRolesView;
        private bool _AccessRolesCreate;

        protected override async Task OnInitializedAsync()
        {
            var state = await authenticationState;
            SetPermissions(state);

            if (!_AccessRolesView)
                _navigationManager.NavigateTo("/noaccess");
        }

        private void SetPermissions(AuthenticationState state)
        {
            _AccessRolesView = securityService.HasPermission(state.User, Access.Roles.View);
            _AccessRolesCreate = securityService.HasPermission(state.User, Access.Roles.Create);
        }

        private async Task<TableData<RoleDto>> ServerReload(TableState state)
        {
            await LoadData(state.Page, state.PageSize, state);
            _loaded = true;
            base.StateHasChanged();
            return new TableData<RoleDto> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {
            string[] orderings = null;
            if (!string.IsNullOrEmpty(state.SortLabel))
            {
                orderings = state.SortDirection != SortDirection.None ? new[] { $"{state.SortLabel} {state.SortDirection}" } : new[] { $"{state.SortLabel}" };
            }

            var request = new PagedRequest { PageSize = pageSize, PageNumber = pageNumber + 1, SearchString = _searchString, Orderby = orderings };
            var response = await securityService.RolesGetAsync(request);
            if (!response.HasError)
            {
                _totalItems = response.Paging.TotalItems;
                _currentPage = response.Paging.CurrentPage;
                _pagedData = response.Result;
            }
            else
            {
                Snackbar.Add(response.Message, Severity.Error);
            }
        }

        private void OnSearch(string text)
        {
            _searchString = text;
            _table.ReloadServerData();
        }

        private void ViewRole(int id)
        {
            _navigationManager.NavigateTo($"/admin/roleviewedit/{id}");
        }

        private void CreateRole()
        {
            _navigationManager.NavigateTo("/admin/rolecreate");
        }
    }
}
