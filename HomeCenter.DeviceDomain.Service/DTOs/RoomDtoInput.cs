using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IRoomInput", Module = "HomeCenter")]
    public class RoomDtoInput
    {
        public string Name { get; set; }
    }
}
