using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain

{
    public class Building : IAggregateRoot
    {
        public long Id { get; set; }
        public virtual List<Floor> Floors { get; set; }
        public string Name { get; set; }
    }
}
