using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain.DomainLayer.RepositoryInterface
{
    public interface IDeviceJobRepository:IGenericRepository<DeviceJob>
    {
        DeviceJob GetDeviceJob(long id);
        void Add(DeviceJob deviceJob);
        void Remove(long id);
        IEnumerable<DeviceJob> GetAll();
    }
}
