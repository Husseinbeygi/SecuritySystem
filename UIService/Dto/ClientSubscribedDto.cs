namespace UIService.Dto
{
    public class ClientSubscribedDto
    {
        public string ClientId { get; set; }
        public string Topic { get; set; }
        public string QualityOfServiceLevel { get; set; }
        public bool Retian { get; set; }
        public string LastConnectedDate { get; set; }

    }
}
