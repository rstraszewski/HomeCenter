using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IDeviceRelationInput", Module = "HomeCenter")]
    public class DeviceRelationDtoInput
    {
        public long GroupActionId { get; set; }
        public long GroupReactionId { get; set; }
        public long IfStateValue { get; set; }
        public long ThenStateValue { get; set; }
        public StateRelation StateRelation { get; set; }
    }
}