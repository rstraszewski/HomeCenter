using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeCenter.DeviceDomain
{
    public interface IDeviceService
    {
        void UpdateDeviceStateFromUi(long deviceId, long deviceState, string username);
        List<Device> GetAllDevicesWithinGroup(Device device);
        List<Device> GetAllDevicesWithinGroup(long groupId);
        List<Device> GetAllDevices(Device device);
        void UpdateDevice(Device device);
        List<DeviceTypeDtoOutput> GetDeviceTypes();
        List<GroupDtoOutput> GetGroups();
        void CreateDevice(DeviceDtoInput deviceInput, string getUsernameFromCookie, GroupDtoInput group = null);
        void SetAllDevicesWithinGroup(long groupId, long state);
        void SetAllDevicesWithinGroup(Device deviceEntity);

        void AddDeviceRelation(DeviceRelationDtoInput entity);
        void RemoveDeviceRelation(long deviceRelationId);
        IEnumerable<DeviceRelationDto> GetAllDeviceRelations();
        void SetDeviceRelationBasedState(long groupId, long deviceState, long lastDeviceState);
        GroupDtoOutput GetGroup(long groupActionId);
        void ChangeDeviceStateWith(Device device);

        void RemoveDevice(long deviceId);
        IEnumerable<DeviceDtoOutput> GetAllDevices();
        Device GetDevice(long address);
        void UpdateDeviceStateFromSerialPort(long address, long state);

        DeviceJobDto GetDeviceJob(long id);
        void AddJob(DeviceJobDtoInput deviceJob);
        void RemoveJob(long id);
        IEnumerable<DeviceJobDto> GetAllJobs();
        void ChangeDeviceGroup(long deviceId, long groupId);

        long GetAddresForPhisicalDevice();
        IEnumerable<long> GetAvailableAddressesForUi();
    }
}
