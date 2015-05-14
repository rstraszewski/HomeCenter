using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IBuilding", Module = "HomeCenter")]
    public class BuildingDtoOutput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<FloorDtoOutput> Floors { get; set; }
    }
}
