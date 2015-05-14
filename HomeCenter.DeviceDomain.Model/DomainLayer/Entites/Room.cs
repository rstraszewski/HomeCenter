using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.DeviceDomain;

namespace HomeCenter.DeviceDomain
{
    public class Room
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long FloorId { get; set; }
        public virtual Floor Floor { get; set; }
    }
}
