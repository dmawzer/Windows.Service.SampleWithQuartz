using System;
using System.Configuration;
using System.Diagnostics;
using Quartz;
using Quartz.Impl;

namespace Windows.Service.Sample
{
    public class QuartzService
    {
        private readonly IScheduler scheduler;
        private const string CRON_TRIGGER_NAME = "SampleCronTrigger";
        private const string JOB_DETAIL_NAME = "SampleJobDetail";

        public QuartzService()
        {
            string cronExpression = ConfigurationSettings.AppSettings["CronExpression"];
            CronTrigger cronTrigger = new CronTrigger(CRON_TRIGGER_NAME, null, cronExpression);
            JobDetail jobDetail = new JobDetail(JOB_DETAIL_NAME, null, typeof(EventLogJob));

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler();
            scheduler.ScheduleJob(jobDetail, cronTrigger);
        }

        public void OnStart()
        {
            scheduler.Start();
        }

        public void OnStop()
        {
            scheduler.Shutdown(true);
        }
    }

    public class EventLogJob : IJob
    {
        private readonly TextWriter textWriter;
        public EventLogJob()
        {
            textWriter = new TextWriter();
        }

        public void Execute(JobExecutionContext context)
        {
            textWriter.Write("MyJob is executed");
        }
    }
}