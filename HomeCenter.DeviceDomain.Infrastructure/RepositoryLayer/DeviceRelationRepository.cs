using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;
using HomeCenter.DeviceDomain;

namespace HomeCenter.RepositoryLayer
{
    public class DeviceRelationRepository: GenericRepository<DeviceRelation>, IDeviceRelationRepository
    {
        public DeviceRelationRepository(IDbContext contextDb) : base(contextDb)
        {
        }


        public void AddDeviceRelation(DeviceRelation entity)
        {
            _set.Add(entity);
        }

        public void RemoveDeviceRelation(long relationId)
        {
            var entity = _set.Find(relationId);
            _set.Remove(entity);
        }

        IEnumerable<DeviceRelation> IDeviceRelationRepository.GetAllDeviceRelations()
        {
            return _set;
        }
    }
}
