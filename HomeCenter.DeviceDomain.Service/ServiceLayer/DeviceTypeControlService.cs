using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace HomeCenter.DeviceDomain 
{
    public class DeviceTypeControlService : IDeviceTypeControlService
    {
        private readonly IDeviceTypeRepository _deviceTypeRepository;

        public DeviceTypeControlService(IDeviceTypeRepository deviceTypeRepository)
        {
            _deviceTypeRepository = deviceTypeRepository;
        }

        public IEnumerable<DeviceTypeDtoOutput> GetAllDeviceTypes()
        {
            var devices = _deviceTypeRepository.GetAllDeviceTypes();
            var data = Mapper.Map<IEnumerable<DeviceTypeDtoOutput>>(devices);
            return data;
        }
    }
}
