using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IFloor", Module = "HomeCenter")]
    public class FloorDtoOutput
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public IEnumerable<RoomDtoOutput> Rooms { get; set; }
    }
}
