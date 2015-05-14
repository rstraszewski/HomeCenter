using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IDevice", Module = "HomeCenter")]
    public class DeviceDtoOutput
    {
        public long Id { get; set; }
        public long DeviceState { get; set; }
        public long RoomId { get; set; }
        public long GroupId { get; set; }
        public string Description { get; set; }
        public string Changeable { get; set; }
        public string GroupName { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public bool Binary { get; set; }
        public string DeviceName { get; set; }
        public long Address { get; set; }
        public bool IsVirtual { get; set; }
    }
}
