using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;
using HomeCenter.HistoryDomain;

namespace HomeCenter.DeviceDomain
{
    public class DeviceStateHasChangedFromUIHandler : IHandles<DeviceStateHasChangedFromUi>
    {
        private readonly IDeviceService _deviceService;
        private readonly IDomainLoggerService _domainLoggerService;
        public DeviceStateHasChangedFromUIHandler(IDeviceService deviceService, IDomainLoggerService domainLoggerService)
        {
            _deviceService = deviceService;
            _domainLoggerService = domainLoggerService;
        }

        public void Handle(DeviceStateHasChangedFromUi args)
        {
            _domainLoggerService.Publish(args.Username, DateTime.Now, string.Format("Change State of devices in group {0} to {1}",args.Device.GroupId, args.Device.DeviceState));
            //_deviceService.SetAllDevicesWithinGroup(args.Device);
            _deviceService.ChangeDeviceStateWith(args.Device);
            //_deviceService.SetDeviceRelationBasedState(args.Device.GroupId, args.Device.DeviceState, args.Device.LastDeviceState);
        }
    }
}
