using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IDeviceRelation", Module = "HomeCenter")]
    public class DeviceRelationDto
    {
        public long Id { get; set; }
        public long GroupActionId { get; set; }
        public long GroupReactionId { get; set; }
        public long IfStateValue { get; set; }
        public long ThenStateValue { get; set; }
        public StateRelation StateRelation { get; set; }
        public string StateRelationDescription { get; set; }
    }
}