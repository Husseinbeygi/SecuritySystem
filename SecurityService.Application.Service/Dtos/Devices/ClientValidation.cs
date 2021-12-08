namespace SecurityService.Application.Service.Dtos.Devices
{
    public class ClientValidation
    {
        public long Id { get; set; }
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}