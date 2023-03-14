using DsK.ITSM.Security.Shared;
using Microsoft.AspNetCore.Components;

namespace DsK.ITSM.Client.Components
{
    public partial class ITSystemsDropdown
    {
        [Parameter]
        public ItsystemDto Value { get; set; }
        private List<ItsystemDto> List;
        private bool _loaded;
        [Parameter]
        public EventCallback<ItsystemDto> ValueChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var Get = await securityService.ITSystemsGetAsync();
            List = Get.Result;
            _loaded = true;
        }

        async Task UpdateValue(ItsystemDto SelectedValue)
        {
            Value = SelectedValue;
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
