using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCenter.DeviceDomain
{
    [ComplexType]
    public class Range
    {
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
    }
}
