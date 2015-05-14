using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;
using HomeCenter.DeviceDomain;

namespace HomeCenter.DeviceDomain
{
    public class DeviceTypeRepository : GenericRepository<DeviceType>, IDeviceTypeRepository
    {
        public DeviceTypeRepository(IDbContext contextDb)
            : base(contextDb)
        {
        }

        public void AddDeviceType(DeviceType entity)
        {
            _set.Add(entity);
        }

        public IEnumerable<DeviceType> GetAllDeviceTypes()
        {
            return _set;
        }

        public DeviceType GetDeviceType(long deviceTypeId)
        {
            return _set.Find(deviceTypeId);
        }
    }
}
