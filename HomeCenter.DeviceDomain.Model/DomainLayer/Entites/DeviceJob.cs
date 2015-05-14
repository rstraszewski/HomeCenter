using System;
using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain
{
    public class DeviceJob:IAggregateRoot
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long State { get; set; }
        public bool IsCron { get; set; }
        public DateTime? DateTime { get; set; }
        public string CronExpression { get; set; }
    }
}