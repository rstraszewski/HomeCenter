using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IBuildingInput", Module = "HomeCenter")]
    public class BuildingDtoInput
    {
        public string Name { get; set; }
    }
}
