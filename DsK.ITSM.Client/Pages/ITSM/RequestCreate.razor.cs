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
    private List<CategoryDto> CategoryList;
    private HashSet<CategoryDto> CategoryListDefaultOptions { get; set; }
    private int userId;

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

        var responseGategoriesGet = await securityService.CategoriesGetAsync();
        CategoryList = responseGategoriesGet.Result;

        CategoryListDefaultOptions = new HashSet<CategoryDto>() { CategoryList.Where(x => x.CategoryName == "Service Request").FirstOrDefault() };

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
        Snackbar.Add(model.Category.CategoryName, Severity.Error);
        //var result = await securityService.RequestCreateAsync(model);

        //if (result != null)
        //    if (result.HasError)
        //        Snackbar.Add(result.Message, Severity.Error);
        //    else
        //    {
        //        Snackbar.Add(result.Message, Severity.Success);
        //        //_navigationManager.NavigateTo($"/ITSM/RoleViewEdit/{result.Result.Id}");
        //    }
        //else
        //    Snackbar.Add("An Unknown Error Has Occured", Severity.Error);
    }
}
