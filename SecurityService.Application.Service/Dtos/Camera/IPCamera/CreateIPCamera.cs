﻿namespace SecurityService.Application.Service.Dtos.Camera.IPCamera
{
    public class CreateIPCamera
    {
        public string HostAddress { get;  set; }
        public string UserName { get;  set; }
        public string Password { get;  set; }
        public string StreamAddress { get;  set; }

    }

}
