namespace IN2.UserPortal.Persistance.Models
{
    public class UserLoginHistoryModel
    {
        public string SessionUuid { get; set; }

        public int UserId { get; set; }
        public string ApplicationType { get; set; }
        public string DevicePlatform { get; set; }
        public string DeviceVersion { get; set; }
        public string DeviceBrand { get; set; }
        public string DeviceModel { get; set; }
        public string Browser { get; set; }
        public string BrowserVersion { get; set; }


    }
}
