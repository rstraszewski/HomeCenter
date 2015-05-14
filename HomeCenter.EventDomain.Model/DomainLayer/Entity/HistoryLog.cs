using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.HistoryDomain
{
    public class HistoryLog : IAggregateRoot
    {
        public string Username { get; set; }
        public virtual DateTime DateTime { get; set; }
        public string Description { get; set; }
        public long Id { get; set; }
    }
}
