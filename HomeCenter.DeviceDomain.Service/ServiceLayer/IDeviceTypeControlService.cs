using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeCenter.DeviceDomain
{
    public interface IDeviceTypeControlService
    {
        IEnumerable<DeviceTypeDtoOutput> GetAllDeviceTypes();
    }
}
