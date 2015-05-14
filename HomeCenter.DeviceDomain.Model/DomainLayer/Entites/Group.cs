using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain
{
    public class Group : IAggregateRoot
    {
        public long Id { get; set; }
        public long Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
