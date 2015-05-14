using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain
{
    public class DeviceStateHasChangedFromSerialPort : IDomainEvent
    {
        public Device Device { get; set; }

        public DeviceStateHasChangedFromSerialPort(Device device)
        {
            Device = device;
        }
    }
}
