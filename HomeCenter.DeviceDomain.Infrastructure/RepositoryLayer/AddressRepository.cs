using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HomeCenter.CommonDomain;
using HomeCenter.DeviceDomain.DomainLayer.RepositoryInterface;

namespace HomeCenter.DeviceDomain
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(IDbContext contextDb)
            : base(contextDb)
        {
        }


        public void Update(Address entity)
        {
            _set.Attach(entity);
            _contextDb.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<Address> GetAvailableAddressesForDevice()
        {
            return _set.Where(address => address.IsAssigned && address.DeviceId == null);
        }

        public Address GetAddressByNumber(long address)
        {
            return _set.First(a => a.AddressNumber==address);
        }

        public IEnumerable<Address> GetAvailableAddressesForPhisicalDevice()
        {
            return _set.Where(address => address.DeviceId == null);
        }
    }
}