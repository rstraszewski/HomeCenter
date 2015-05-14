using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain
{
    public class DeviceStateHasChanged : IDomainEvent
    {
        public Device Device { get; set; }
        public string Username { get; set; }
        public bool onlyWebsocket { get; set; }
    }
}