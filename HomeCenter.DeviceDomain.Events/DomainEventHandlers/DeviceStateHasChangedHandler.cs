using System;
using System.Threading.Tasks;
using AutoMapper;
using HomeCenter.CommonDomain;
using HomeCenter.HistoryDomain;
using HomeCenter.Communication;
namespace HomeCenter.DeviceDomain
{
    public class DeviceStateHasChangedHandler : IHandles<DeviceStateHasChanged>, IDomainEvent
    {
        private readonly ISerialPortConnection _serialPortConnection;
        private readonly IWebSocket _webSocket;
        public DeviceStateHasChangedHandler(ISerialPortConnection serialPortConnection, IWebSocket webSocket)
        {
            _serialPortConnection = serialPortConnection;
            _webSocket = webSocket;
        }

        public void Handle(DeviceStateHasChanged args)
        {
            var deviceDto = Mapper.Map<HomeCenter.DeviceDomain.DeviceDtoOutput>(args.Device);
            if (!deviceDto.IsVirtual && !args.onlyWebsocket)
                _serialPortConnection.AddDeviceToUpdate(deviceDto);
            _webSocket.AddDeviceToUpdate(deviceDto);
        }
    }
}