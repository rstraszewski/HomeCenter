using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.DeviceDomain;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IRoom", Module = "HomeCenter")]
    public class RoomDtoOutput
    {
        public string Name { get; set; }
        public long Id { get; set; }
    }
}
