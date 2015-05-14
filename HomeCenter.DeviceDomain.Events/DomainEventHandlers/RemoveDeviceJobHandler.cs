using AutoMapper;
using HomeCenter.CommonDomain;
using HomeCenter.Communication;

namespace HomeCenter.DeviceDomain
{
    public class RemoveDeviceJobHandler : IHandles<RemoveDeviceJob>, IDomainEvent
    {
        private readonly IJobScheduler _jobScheduler;

        public RemoveDeviceJobHandler(IJobScheduler jobScheduler)
        {
            _jobScheduler = jobScheduler;
        }

        public void Handle(RemoveDeviceJob args)
        {
            _jobScheduler.RemoveJob(args.DeviceJobId);
        }
    }
}