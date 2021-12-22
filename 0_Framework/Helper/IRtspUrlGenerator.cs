using System.Collections.Generic;

namespace _0_Framework.Helper
{
    public interface IRtspUrlGenerator
    {
        string GenerateUrl(string host, string username, string password, string liveaddress);
        List<string> returnlist();
    }
}