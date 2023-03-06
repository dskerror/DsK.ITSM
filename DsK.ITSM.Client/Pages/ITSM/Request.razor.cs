using DsK.ITSM.Security.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using DsK.ITSM.Security.Shared.Constants;
using DsK.ITSM.Client.Services.Requests;

namespace DsK.ITSM.Client.Pages.ITSM;
public partial class Request
{
    [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; }
    [Parameter] public int id { get; set; }
    public RequestDto model { get; set; }
    private List<UserDto> RequestedByList;
    private bool _loaded;
    private ItsystemDto itsystemDto;
    private PriorityDto priorityDto;
    private CategoryDto categoryDto;

    private List<BreadcrumbItem> _breadcrumbs = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("ITSM", href: "/"),
            new BreadcrumbItem("Request", href: null, disabled: true)
        };
    protected override async Task OnInitializedAsync()
    {
        var state = await authenticationState;
        PagedRequest request = new PagedRequest() { Id= id };
        var result = await securityService.RequestsGetAsync(request);
        var responseRequestedByUserList = await securityService.RequestedByUserListGetAsync();
        RequestedByList = responseRequestedByUserList.Result;
        //model.RequestedBy = await SearchUserNameById(userId);

        _loaded = true;
    }

    private async Task<string> SearchUserNameById(int UserId)
    {
        return RequestedByList.Where(x => x.Id == UserId).FirstOrDefault().Name;
    }
    private async Task<IEnumerable<string>> SearchRequestedByList(string value)
    {
        return RequestedByList.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Name).ToList();
    }
    //private async Task EditRole()
    //{
    //    //var result = await securityService.RoleEditAsync(role);

    //    //if (result != null)
    //    //    if (result.HasError)
    //    //        Snackbar.Add(result.Message, Severity.Error);
    //    //    else
    //    //        Snackbar.Add(result.Message, Severity.Success);
    //    //else
    //    //    Snackbar.Add("An Unknown Error Has Occured", Severity.Error);
    //}
}
