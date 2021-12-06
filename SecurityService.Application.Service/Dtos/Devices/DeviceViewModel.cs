namespace SecuritySystem.Application.Dtos.Devices
{
    public class DeviceViewModel
    {
        public long Id { get; set; }
        public string DeviceId { get; set; }
        public string UserName { get; set; }
        public bool IsOnline { get; set; }
        public DateTime CreationDate { get; set; }
    }

}

