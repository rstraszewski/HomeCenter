using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite;

namespace HomeCenter.DeviceDomain
{
    [TsClass(Name = "IGroup", Module = "HomeCenter")]
    public class GroupDtoOutput
    {
        public long Id { get; set; }
        public long Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
