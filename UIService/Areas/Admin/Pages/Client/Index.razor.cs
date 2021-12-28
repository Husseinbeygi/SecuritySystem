using SecurityService.Application.Service.Client;

namespace UIService.Areas.Admin.Pages.Client
{
    public partial class Index
    {
        public ClientSearchModel _searchModel { get; set; }

        public Index()
        {
        }

        protected override void OnInitialized()
        {
            _searchModel = new ClientSearchModel();
        }


        public void OnDeleteClient(long id)
        {
            _application.Remove(id);
            StateHasChanged();
        }

    }
}
