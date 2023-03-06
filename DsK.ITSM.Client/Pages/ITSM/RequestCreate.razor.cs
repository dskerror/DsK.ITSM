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
    private List<UserDto> RequestedByList;
    private int userId;
    private ItsystemDto itsystemDto;
    private PriorityDto priorityDto;
    private CategoryDto categoryDto;

    protected override async Task OnInitializedAsync()
    {
        var state = await authenticationState;
        userId = securityService.GetUserId(state.User);
        SetPermissions(state);

        if (!_AccessRequestCreate)
            _navigationManager.NavigateTo("/noaccess");

        var responseRequestedByUserList = await securityService.RequestedByUserListGetAsync();
        RequestedByList = responseRequestedByUserList.Result;
        model.RequestedBy = await SearchUserNameById(userId);
     
        _loaded = true;
    }

    private void SetPermissions(AuthenticationState state)
    {
        _AccessRequestCreate = true;
        //_AccessRequestCreate = securityService.HasPermission(state.User, Access.Requests.Create);
    }

    private async Task<string> SearchUserNameById(int UserId)
    {
        return RequestedByList.Where(x => x.Id == UserId).FirstOrDefault().Name;
    }
    private async Task<IEnumerable<string>> SearchRequestedByList(string value)
    {
        return RequestedByList.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Name).ToList();
    }
    private async Task Create()
    {
        model.RequestedByUserId= userId;
        model.RequestTypeId = 1;
        model.CategoryId = categoryDto.Id;
        model.PriorityId = priorityDto.Id;
        model.ItsystemId = itsystemDto.Id;

        var result = await securityService.RequestCreateAsync(model);

        if (result != null)
            if (result.HasError)
                Snackbar.Add(result.Message, Severity.Error);
            else
            {
                Snackbar.Add(result.Message, Severity.Success);
                //_navigationManager.NavigateTo($"/ITSM/RequestsViewEdit/{result.Result.Id}");
            }
        else
            Snackbar.Add("An Unknown Error Has Occured", Severity.Error);
    }
}
