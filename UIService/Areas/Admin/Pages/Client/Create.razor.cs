using SecurityService.Application.Service.Client;

namespace UIService.Areas.Admin.Pages.Client
{
    public partial class Create
    {
        public CreateClient _createClient { get; set; }

        public Create()
        {
        }

        protected override void OnInitialized()
        {
            _createClient = new CreateClient();
        }

        private void HandleValidSubmit()
        {
            var op = _application.Create(_createClient);
            if (op.IsSuccedded)
            {
                NavManager.NavigateTo("/admin/client");
            }

        }
    }
}
