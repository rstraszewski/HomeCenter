using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IFloorInput", Module = "HomeCenter")]
    public class FloorDtoInput
    {
        public string Name { get; set; }
    }
}
