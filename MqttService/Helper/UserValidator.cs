using SecuritySystem.Domain.Client;

namespace MqttService.Helper
{
    public class UserValidator
    {
        private readonly Client _user;

        public UserValidator(Client user)
        {
            _user = user;
        }


        public bool IsUserNull()
        {
            return _user == null;
        }
    }



}
