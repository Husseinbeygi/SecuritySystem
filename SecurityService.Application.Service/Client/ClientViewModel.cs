namespace SecurityService.Application.Service.Client
{
    public class ClientViewModel
    {
        public long Id { get; set; }
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public bool IsOnline { get; set; }
        public string CreationDate { get; set; }
    }

}

