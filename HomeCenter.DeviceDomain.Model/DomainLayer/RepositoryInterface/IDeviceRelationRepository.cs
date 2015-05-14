using System.Collections.Generic;
using HomeCenter.CommonDomain;
using HomeCenter.DeviceDomain;

namespace HomeCenter.RepositoryLayer
{
    public interface IDeviceRelationRepository: IGenericRepository<DeviceRelation>
    {
        void AddDeviceRelation(DeviceRelation entity);
        void RemoveDeviceRelation(long entity);
        IEnumerable<DeviceRelation> GetAllDeviceRelations();
    }
}