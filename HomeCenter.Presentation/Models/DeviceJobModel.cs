using TypeLite;

namespace HomeCenter.Models
{
    [TsClass(Name = "IDeviceJobModel", Module = "HomeCenter")]
    public class DeviceJobModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
    }
}