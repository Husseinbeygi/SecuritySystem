using DeviceManagment.Domain.Device;

namespace MqttService.Helper
{
    public class UserValidator
    {
        private readonly User _user;

        public UserValidator(User user)
        {
            _user = user;
        }


        public bool IsUserNull()
        {
            return _user == null;
        }
    }



}
