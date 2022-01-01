using Microsoft.AspNetCore.Components;
using SecurityService.Application.Service.Client;

namespace UIService.Areas.Admin.Pages.Client
{
    public partial class ChangeClientPassword
    {
        public ChangeClientPassword()
        {
        }

        [Parameter]
        public long id { get; set; }

        public ChangePassword _changePassword { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            _changePassword = new ChangePassword(); 
        }

        public void HandleValidSubmit()
        {
            _changePassword.Id = id;
            var op = _application.ChangePassword(_changePassword);
            if (op.IsSuccedded)
            {
                NavManager.NavigateTo("/admin/client");
            }

        }
    }
}
