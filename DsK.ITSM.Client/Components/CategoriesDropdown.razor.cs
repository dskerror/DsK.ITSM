using DsK.ITSM.Security.Shared;
using Microsoft.AspNetCore.Components;

namespace DsK.ITSM.Client.Components
{
    public partial class CategoriesDropdown
    {
        [Parameter]
        public CategoryDto Value { get; set; }
        private List<CategoryDto> List;
        private bool _loaded;
        [Parameter]
        public EventCallback<CategoryDto> ValueChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var Get = await securityService.CategoriesGetAsync();
            List = Get.Result;
            UpdateValue(List.Where(x => x.CategoryName == "Service Request").FirstOrDefault());
            _loaded = true;
        }

        async Task UpdateValue(CategoryDto SelectedValue)
        {
            Value = SelectedValue;
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
