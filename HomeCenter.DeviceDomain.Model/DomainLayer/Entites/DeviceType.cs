using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeCenter.DeviceDomain
{
    public class DeviceType : IAggregateRoot
    {
        public string Type { get; set; }
        public long Id { get; set; }
        public bool Changable { get; set; }
        public bool Binary { get; set; }
        public Range Range { get; set; }
        public string Description { get; set; }
        public bool IsVirtual { get; set; }
    }

    
}
