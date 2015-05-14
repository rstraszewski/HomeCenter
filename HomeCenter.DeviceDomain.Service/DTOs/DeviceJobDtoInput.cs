using System;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IDeviceJobInput", Module = "HomeCenter")]
    public class DeviceJobDtoInput
    {
        public long GroupId { get; set; }
        public long State { get; set; }
        public bool IsCron { get; set; }
        public long? DateTime { get; set; }
        public string CronExpression { get; set; }
    }
}