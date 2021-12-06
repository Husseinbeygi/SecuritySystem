using SecuritySystem.Domain.Device;

namespace MqttService.Helper
{
    public class UserValidator
    {
        private readonly Device _user;

        public UserValidator(Device user)
        {
            _user = user;
        }


        public bool IsUserNull()
        {
            return _user == null;
        }
    }



}
