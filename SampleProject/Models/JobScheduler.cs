using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleProject.Models;


namespace SampleProject.Models
{
    public class JobScheduler
    {
        public  void Start()
        {
            IJobDetail emailJob = JobBuilder.Create<MailJob>()
                  .WithIdentity("job1")
                  .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInSeconds(30)
                    .OnEveryDay()
                  )
                 .ForJob(emailJob)
                 .WithIdentity("trigger1")
                 .StartNow()
                 .WithCronSchedule("0 0 8 * * ?") // Time : Every 1 Minutes job execute
                 .Build();

            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sc = sf.GetScheduler();
            sc.ScheduleJob(emailJob, trigger);
            sc.Start();
        }
    }
}