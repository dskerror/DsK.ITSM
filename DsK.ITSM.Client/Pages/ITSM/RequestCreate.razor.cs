using DsK.ITSM.Security.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using DsK.ITSM.Security.Shared.Constants;

namespace DsK.ITSM.Client.Pages.ITSM;
public partial class RequestCreate
{
    [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; }
    private RequestCreateDto model = new RequestCreateDto();
    private bool _loaded;
    private bool _AccessRequestCreate;
    private List<UserDto> RequestedByUserList;

    protected override async Task OnInitializedAsync()
    {
        var state = await authenticationState;
        SetPermissions(state);

        if (!_AccessRequestCreate)
            _navigationManager.NavigateTo("/noaccess");

        var response = await securityService.RequestedByUserListGetAsync();
        RequestedByUserList = response.Result;
        _loaded = true;
    }

    private void SetPermissions(AuthenticationState state)
    {
        _AccessRequestCreate = true;
        //_AccessRequestCreate = securityService.HasPermission(state.User, Access.Requests.Create);
    }

    private async Task<IEnumerable<string>> SearchRequestedByUserList(string value)
    {   
        return RequestedByUserList.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Name).ToList();
    }

    private async Task<IEnumerable<UserDto>> SearchRequestedByUserList2(string value)
    {
        return RequestedByUserList.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private async Task Create()
    {
        //var result = await securityService.Re(model);

        //if (result != null)
        //    if (result.HasError)
        //        Snackbar.Add(result.Message, Severity.Error);
        //    else
        //    {
        //        Snackbar.Add(result.Message, Severity.Success);
        //        _navigationManager.NavigateTo($"/admin/roleviewedit/{result.Result.Id}");
        //    }
        //else
        //    Snackbar.Add("An Unknown Error Has Occured", Severity.Error);

    }
}
