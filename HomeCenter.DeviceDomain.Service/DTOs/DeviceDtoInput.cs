using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IDeviceInput", Module = "HomeCenter")]
    public class DeviceDtoInput
    {
        public long DeviceTypeId { get; set; }
        public long RoomId { get; set; }
        public long BuildingId { get; set; }
        public long FloorId { get; set; }
        public long? GroupId { get; set; }
        public long Address { get; set; }
        public string Name { get; set; }
    }

    
}
