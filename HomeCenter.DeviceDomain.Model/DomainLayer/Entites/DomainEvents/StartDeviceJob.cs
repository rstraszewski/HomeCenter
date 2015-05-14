using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain
{
    public class StartDeviceJob : IDomainEvent
    {
        public DeviceJob DeviceJob { get; set; }
        //public string Username { get; set; }
    }
    public class RemoveDeviceJob : IDomainEvent
    {
        public long DeviceJobId { get; set; }
        //public string Username { get; set; }
    }
}