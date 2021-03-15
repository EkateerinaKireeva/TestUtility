using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;


namespace GismeteoGrabber.Scheduler
{
    public static class ParserScheduler
    {
        public static async void Start(IServiceProvider serviceProvider)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = serviceProvider.GetRequiredService<JobFactory>();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<ParserJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("MailingTrigger", "default")
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(3)
                .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
