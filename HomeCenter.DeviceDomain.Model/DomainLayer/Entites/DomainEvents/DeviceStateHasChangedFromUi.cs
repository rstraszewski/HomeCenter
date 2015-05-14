using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain
{
    public class DeviceStateHasChangedFromUi : IDomainEvent
    {
        public Device Device { get; set; }
        public string Username { get; set; }
    }
}
