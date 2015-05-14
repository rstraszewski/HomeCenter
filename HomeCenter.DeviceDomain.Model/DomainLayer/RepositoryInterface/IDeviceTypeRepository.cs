using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain
{
    public interface IDeviceTypeRepository : IGenericRepository<DeviceType>
    {
        void AddDeviceType(DeviceType entity);
        IEnumerable<DeviceType> GetAllDeviceTypes();
        DeviceType GetDeviceType(long deviceTypeId);
    }
}
