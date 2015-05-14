using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain.DomainLayer.RepositoryInterface
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        void Update(Address entity);
        IEnumerable<Address> GetAvailableAddressesForPhisicalDevice();
        IEnumerable<Address> GetAvailableAddressesForDevice();
        Address GetAddressByNumber(long address);
    }
}
