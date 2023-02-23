using DsK.ITSM.Client.Services.Requests;
using DsK.ITSM.Security.Shared.Constants;
using DsK.ITSM.Security.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DsK.ITSM.Client.Components
{
    public partial class MyRequests
    {
        [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; }
        private IEnumerable<RequestDto> _pagedData;
        private MudTable<RequestDto> _table;
        private bool _loaded;
        private int _totalItems;
        private int _currentPage;
        private string _searchString = "";
        private bool _AccessRequestView;
        private bool _AccessRequestCreate;
        private int userId;

        protected override async Task OnInitializedAsync()
        {
            var state = await authenticationState;
            userId = securityService.GetUserId(state.User);
            SetPermissions(state);

            if (!_AccessRequestView)
                _navigationManager.NavigateTo("/noaccess");
        }

        private void SetPermissions(AuthenticationState state)
        {
            _AccessRequestView = true;
            _AccessRequestCreate = true;
            //_AccessRequestView = securityService.HasPermission(state.User, Access.Requests.View);
            //_AccessRequestCreate = securityService.HasPermission(state.User, Access.Requests.Create);

        }

        private async Task<TableData<RequestDto>> ServerReload(TableState state)
        {
            await LoadData(state.Page, state.PageSize, state);
            _loaded = true;
            base.StateHasChanged();
            return new TableData<RequestDto> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {
            string[] orderings = null;
            if (!string.IsNullOrEmpty(state.SortLabel))
            {
                orderings = state.SortDirection != SortDirection.None ? new[] { $"{state.SortLabel} {state.SortDirection}" } : new[] { $"{state.SortLabel}" };
            }
            var request = new PagedRequest { Id = userId, PageSize = pageSize, PageNumber = pageNumber + 1, SearchString = _searchString, Orderby = orderings };
            var response = await securityService.RequestsGetAsync(request);
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

        private void ViewRequest(int id)
        {
            _navigationManager.NavigateTo($"/ITSM/RequestViewEdit/{id}");
        }

        private void CreateRequest()
        {
            _navigationManager.NavigateTo("/ITSM/RequestCreate");
        }
    }
}
