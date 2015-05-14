using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite;

namespace HomeCenter.HistoryDomain
{
    [TsClass(Name = "IHistoryLog", Module = "HomeCenter")]
    public class HistoryLogDto
    {
        public string Username { get; set; }
        public string DateTime { get; set; }
        public string Description { get; set; }
        public long Id { get; set; }
    }
}
