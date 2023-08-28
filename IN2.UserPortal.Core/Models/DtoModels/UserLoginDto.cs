namespace IN2.UserPortal.Core.Models.DtoModels
{
    public class UserLoginDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public DeviceInfoDto? DeviceModel { get; set; } = new DeviceInfoDto();

        public string SessionUuid { get; set; }

    }
}
