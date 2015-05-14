using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter;
using HomeCenter.CommonDomain;
namespace HomeCenter.DeviceDomain
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
    //    void Create(Device device);
    //    void Update(Device device);
    //    void Delete(Device device);
    //    void Read(long id);
        //IList<Device> GetAll();

        //IList<Device> Get();
        //void RollbackChanges();
        void Delete(Device entity);
        void AddDevice(Device entity);
        void Update(Device entity);
        Device GetById(long id);
        IEnumerable<Device> GetAllDevices();
        IEnumerable<Device> GetVirtualDevices();
        IEnumerable<long> GetRoomsIdWithDevices();
        //void Save();
        void RemoveDevice(long deviceId);
        Device GetByAddres(long address);
    }

}
