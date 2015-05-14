using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain
{
    public class Address : IAggregateRoot
    {
        public long Id { get; set; }
        public bool IsAssigned { get; set; }
        public long? DeviceId { get; set; }

        public long AddressNumber { get; set; }

        public void SetAssigned()
        {
            IsAssigned = false;
        }

        public void DetachFromDevice()
        {
            DeviceId = null;
        }
    }
}
