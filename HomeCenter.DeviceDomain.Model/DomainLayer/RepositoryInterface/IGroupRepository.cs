using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain.DomainLayer.RepositoryInterface
{
    public interface IGroupRepository : IGenericRepository<Group>
    {
        void AddGroup(Group entity);
        IEnumerable<Group> GetAllGroup();
        Group GetGroup(long groupId);
    }
}
