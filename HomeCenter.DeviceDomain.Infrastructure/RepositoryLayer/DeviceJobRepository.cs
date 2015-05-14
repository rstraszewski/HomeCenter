using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;
using HomeCenter.DeviceDomain;
using HomeCenter.DeviceDomain.DomainLayer.RepositoryInterface;

namespace HomeCenter.RepositoryLayer
{
    public class DeviceJobRepository:GenericRepository<DeviceJob>, IDeviceJobRepository
    {
        public DeviceJobRepository(IDbContext contextDb) : base(contextDb)
        {
        }

        public DeviceJob GetDeviceJob(long id)
        {
            return _set.Find(id);
        }

        public void Add(DeviceJob deviceJob)
        {
            _set.Add(deviceJob);
        }

        public void Remove(long id)
        {
            var entity = _set.Find(id);
            _set.Remove(entity);
        }

        public IEnumerable<DeviceJob> GetAll()
        {
            return _set;
        }
    }
}
