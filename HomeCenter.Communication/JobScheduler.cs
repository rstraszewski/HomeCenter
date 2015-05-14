using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;
using HomeCenter.DeviceDomain;
using HomeCenter.HistoryDomain;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Quartz;
using Quartz.Impl;

namespace HomeCenter.Communication
{

    public class ChangeGroupStateJob : IJob
    {
        private readonly IDomainLoggerService _domainLoggerService;
        private readonly IDeviceService _deviceService;
        public ChangeGroupStateJob(IDomainLoggerService domainLoggerService, IDeviceService deviceService)
        {
            this._domainLoggerService = domainLoggerService;
            _deviceService = deviceService;
        }

        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            //JobKey key = context.JobDetail.Key;
            var id = dataMap.GetLongValue("jobId");

            var job = _deviceService.GetDeviceJob(id);
            if (job != null)
            {
                Console.WriteLine("Set state to" + id + " in group " + job.GroupId);
                //Debug.WriteLine("Set state to" + id + " in group " + job.GroupId);
                _deviceService.SetAllDevicesWithinGroup(job.GroupId, job.State);
                if (!job.IsCron)
                    _deviceService.RemoveJob(id);
            }
            //_domainLoggerService.Publish("admin","exception: mam id + " + id);
            //_deviceService.GetDeviceJob(id);
            //throw new Exception("exception: mam id + " + id);
        }
    }

    public interface IJobScheduler
    {
        void AddJob(DeviceJobDto dJob);
        void RemoveJob(long id);
        void Initialise();
    }

    public class JobScheduler : IJobScheduler
    {

        private readonly IUnityContainer container;
        private readonly IDeviceService _deviceService;
        public JobScheduler(IUnityContainer container, 
            IDeviceService deviceService)
        {
            this.container = container;
            _deviceService = deviceService;
        }

        public ISchedulerFactory schedFact { get; set; }
        public IScheduler sched { get; set; }
        public void Initialise()
        {
            schedFact = container.Resolve<ISchedulerFactory>();
            sched = container.Resolve<IScheduler>();
            schedFact = new StdSchedulerFactory();
            sched = schedFact.GetScheduler();
            sched.Start();
            _deviceService.GetAllJobs().ForEach(AddJob);
            
        }



        public void AddJob(DeviceJobDto dJob)
        {
            var job = JobBuilder.Create<ChangeGroupStateJob>()
                    .WithIdentity(dJob.Id.ToString())
                    .UsingJobData("jobId", dJob.Id)
                    .Build();
            if (!dJob.IsCron)
            {
                var trigger = TriggerBuilder.Create()
                    .WithIdentity(dJob.Id.ToString())
                    .StartAt(dJob.DateTime)
                    .Build();
                sched.ScheduleJob(job, trigger);
            }
            else
            {
                var trigger = TriggerBuilder.Create()
                    .WithIdentity(dJob.Id.ToString())
                    .WithCronSchedule(dJob.CronExpression)
                    .Build();
                sched.ScheduleJob(job, trigger);
            }

        }

        public void RemoveJob(long id)
        {
            sched.DeleteJob(new JobKey(id.ToString()));
        }
    }
}
