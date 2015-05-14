using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace HomeCenter.DeviceDomain.Service.ServiceLayer
{
    public class CreateControlUiService : ICreateControlUiService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IDeviceRepository _deviceRepository;

        public CreateControlUiService(IBuildingRepository buildingRepository, IDeviceRepository deviceRepository)
        {
            _buildingRepository = buildingRepository;
            _deviceRepository = deviceRepository;
        }

        public IEnumerable<DeviceDtoOutput> GetDevicesForUiCreating()
        {
            var deviceForUi = Mapper.Map<IEnumerable<DeviceDtoOutput>>(_deviceRepository.GetVirtualDevices());
            return deviceForUi;
        }

        public IEnumerable<Building> GetAllBuildings()
        {
            return _buildingRepository.GetAllBuildings();
        }
    }
}
