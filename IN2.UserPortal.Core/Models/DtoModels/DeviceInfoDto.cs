using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IN2.UserPortal.Core.Models.DtoModels
{
    public class DeviceInfoDto
    {
        public string? ApplicationType { get; set; }
        public string? DevicePlatform { get; set; }
        public string? DeviceVersion { get; set; }
        public string? DeviceBrand { get; set; }
        public string? DeviceModel { get; set; }
        public string? Browser { get; set; }
        public string? BrowserVersion { get; set; }
    }
}
