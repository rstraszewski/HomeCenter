using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IDeviceType", Module = "HomeCenter")]
    public class DeviceTypeDtoOutput
    {
        public string Type { get; set; }
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsVirtual { get; set; }
    }
}
