using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.IO;

namespace DsK.ITSM.Client.Components
{
    public partial class PriorityDropdown
    {
        [Parameter]
        public PriorityDto Value { get; set; }
        private List<PriorityDto> List;
        private bool _loaded;
        [Parameter]
        public EventCallback<PriorityDto> ValueChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var Get = await securityService.PrioritiesGetAsync();
            List = Get.Result;
            UpdateValue(List.Where(x => x.PriorityName == "Medium").FirstOrDefault());            
            _loaded = true;
        }

        async Task UpdateValue(PriorityDto SelectedValue)
        {
            Value = SelectedValue;
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
