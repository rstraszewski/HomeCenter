using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using HomeCenter.CommonDomain;
using HomeCenter.Database;
using System.Data.Entity.Infrastructure;

namespace HomeCenter.DeviceDomain
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(IDbContext contextDb)
            : base(contextDb)
        {
        }

        public void Delete(Device entity)
        {
            _set.Remove(entity);
        }

        public void AddDevice(Device entity)
        {
            _contextDb.CreateDatabaseIfNotExisting();
            _set.Add(entity);
        }

        public Device GetById(long id)
        {
            return _set.Find(id);
        }

        public IEnumerable<Device> GetAllDevices()
        {
            return _set;
        }

        public IEnumerable<Device> GetVirtualDevices()
        {
            return _set.Where(device => device.DeviceType.IsVirtual);
        }

        public Device GetDeviceById(long Id)
        {
            return _set.FirstOrDefault(device => device.Id == Id);
        }

        public IEnumerable<long> GetRoomsIdWithDevices()
        {
            var roomList = _set.Select(device => device.RoomId).ToList();
            var result = roomList.Distinct();
            return result;
        }

        public void RemoveDevice(long deviceId)
        {
            var entity = _set.Find(deviceId);
            _set.Remove(entity);
        }

        public Device GetByAddres(long address)
        {
            return _set.FirstOrDefault(device => device.Address == address);
        }

        public void Update(Device entity)
        {
            _set.Attach(entity);
            _contextDb.Entry(entity).State = EntityState.Modified;
        }
    }
}
