using AutoMapper;
using HomeCenter.CommonDomain;
using HomeCenter.Communication;

namespace HomeCenter.DeviceDomain
{
    public class StartDeviceJobHandler : IHandles<StartDeviceJob>, IDomainEvent
    {
        private readonly IJobScheduler _jobScheduler;

        public StartDeviceJobHandler(IJobScheduler jobScheduler)
        {
            _jobScheduler = jobScheduler;
        }

        public void Handle(StartDeviceJob args)
        {
            var deviceJobDto = Mapper.Map<HomeCenter.DeviceDomain.DeviceJobDto>(args.DeviceJob);
            _jobScheduler.AddJob(deviceJobDto);
        }
    }
}