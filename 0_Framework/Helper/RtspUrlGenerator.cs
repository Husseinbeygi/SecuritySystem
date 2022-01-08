using System.Collections.Generic;

namespace _0_Framework.Helper
{
    public class RtspUrlGenerator : IRtspUrlGenerator
    {
        List<string> RtspList = new List<string>
        {
        "/12",
        "/live1.264",
        "/live0.264",
        "/11",
        "/profile0",
        "/H264",
        "/0",
        "/1",
        "/video.mp4",
        "/1/h264major",
        "/main",
        "/onvif1",
        "/h264_stream",
        "/videoMain",
        "/cam1/mpeg4",
        "/12",
        "/live.sdp",
        "/profile1",
        "/ucast/11",
        }; 
        //};

        public List<string> returnlist()
        {
            //RtspList.Add(new RtspUrlModel {Url = "/live0.264"});
            //RtspList.Add(new RtspUrlModel({Url = "/11"});
            //RtspList.Add(new RtspUrlModel({Url = "/profile0"});
            //RtspList.Add(new RtspUrlModel({Url = "/live1.264"});
            //RtspList.Add(new RtspUrlModel({Url = "/H264"});
            //RtspList.Add(new RtspUrlModel({Url = "/0"});
            //RtspList.Add(new RtspUrlModel({Url = "/12"});
            //RtspList.Add(new RtspUrlModel({Url = "/1"});
            //RtspList.Add(new RtspUrlModel({Url = "/video.mp4"});
            //RtspList.Add(new RtspUrlModel({Url = "/1/h264major"});
            //RtspList.Add(new RtspUrlModel({Url = "/main"});
            //RtspList.Add(new RtspUrlModel({Url = "/onvif1"});
            //RtspList.Add(new RtspUrlModel({Url = "/h264_stream"});
            //RtspList.Add(new RtspUrlModel({Url = "/videoMain"});
            //RtspList.Add(new RtspUrlModel({Url = "/cam1/mpeg4"});
            //RtspList.Add(new RtspUrlModel({Url = "/12"});
            //RtspList.Add(new RtspUrlModel({Url = "/live.sdp"});
            //RtspList.Add(new RtspUrlModel({Url = "/profile1"});
            //RtspList.Add(new RtspUrlModel({Url = "/ucast/11" });

            return RtspList;
        }

        public string GenerateUrl(string host, string username, string password, string liveaddress)
        {
            string protocol = "rtsp://";
            string port = "8554";
            return protocol + username + ":" + password + "@" + host + ":" + port + liveaddress;
        }

    }


    internal class RtspUrlModel
    {
        public string Url { get; set; }
    }

}
