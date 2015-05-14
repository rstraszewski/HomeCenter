using System;
using HomeCenter.CommonDomain;
using HomeCenter.Communication;
using HomeCenter.HistoryDomain;

namespace HomeCenter.DeviceDomain
{
    public class DeviceStateHasChangedFromSerialPortHandler : IHandles<DeviceStateHasChangedFromSerialPort>, IDomainEvent
    {
        private readonly ISerialPortConnection _serialPortConnection;
        private readonly IWebSocket _webSocket;
        private readonly IDeviceService _deviceService;
        private readonly IDomainLoggerService _domainLoggerService;
        public DeviceStateHasChangedFromSerialPortHandler(ISerialPortConnection serialPortConnection, IWebSocket webSocket, IDomainLoggerService domainLoggerService, IDeviceService deviceService)
        {
            _serialPortConnection = serialPortConnection;
            _webSocket = webSocket;
            _domainLoggerService = domainLoggerService;
            _deviceService = deviceService;
        }

        public void Handle(DeviceStateHasChangedFromSerialPort args)
        {
            _domainLoggerService.Publish(null, DateTime.Now, string.Format("Change State of devices in group {0} to {1} from serial port", args.Device.GroupId, args.Device.DeviceState));
            //_deviceService.SetAllDevicesWithinGroup(args.Device);
            _deviceService.ChangeDeviceStateWith(args.Device);
            //_deviceService.SetDeviceRelationBasedState(args.Device.GroupId, args.Device.DeviceState, args.Device.LastDeviceState);
        }
    }
}