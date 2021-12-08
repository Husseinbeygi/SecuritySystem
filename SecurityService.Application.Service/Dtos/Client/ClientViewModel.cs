namespace SecurityService.Application.Service.Dtos.Client
{
    public class ClientViewModel
    {
        public long Id { get; set; }
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public bool IsOnline { get; set; }
        public DateTime CreationDate { get; set; }
    }

}

