using DsK.ITSM.Security.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace DsK.ITSM.Client.Components
{
    public partial class RequestedByAutoComplete
    {
        [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; }
        private UserDto RequestedByUser;
        private List<UserDto> RequestedByList;
        private int userId;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            var state = await authenticationState;
            userId = securityService.GetUserId(state.User);

            var responseRequestedByUserList = await securityService.RequestedByUserListGetAsync();
            RequestedByList = responseRequestedByUserList.Result;
            RequestedByUser = await SearchUserNameById(userId);

            _loaded = true;
        }
        private async Task<IEnumerable<UserDto>> SearchRequestedByList(string value)
        {
            return RequestedByList.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        private async Task<UserDto> SearchUserNameById(int UserId)
        {
            return RequestedByList.Where(x => x.Id == UserId).FirstOrDefault();
        }
    }
}
