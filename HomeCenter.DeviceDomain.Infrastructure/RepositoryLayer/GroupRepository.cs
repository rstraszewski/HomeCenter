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
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(IDbContext contextDb) : base(contextDb)
        {
        }


        public void AddGroup(Group entity)
        {
            _set.Add(entity);

        }

        public IEnumerable<Group> GetAllGroup()
        {
            return _set;
        }

        public Group GetGroup(long groupId)
        {
            return _set.Find(groupId);
        }
    }
}
