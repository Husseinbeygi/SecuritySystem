using Microsoft.AspNetCore.Components;
using SecurityService.Application.Service.Client;

namespace UIService.Areas.Admin.Pages.Client
{
    public partial class Edit
    {
        [Parameter]
        public long id { get; set; }

        public EditClient _editClient { get; set; }

        protected override void OnInitialized()
        {
            _editClient = _application.GetDetails(id);
        }

        public void HandleValidSubmit()
        {
            _application.Edit(_editClient);
            NavManager.NavigateTo("/admin/client");

        }
    }
}
