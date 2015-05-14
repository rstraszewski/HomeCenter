using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IDeviceJob", Module = "HomeCenter")]
    public class DeviceJobDto
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long State { get; set; }
        public bool IsCron { get; set; }
        public DateTime DateTime { get; set; }
        public string CronExpression { get; set; }
    }
}
