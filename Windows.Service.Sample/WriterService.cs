using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using Quartz;
using Quartz.Impl;

namespace Windows.Service.Sample
{
    public partial class WriterService : ServiceBase
    {
        private readonly IScheduler scheduler;
        private const string CRON_TRIGGER_NAME = "SampleCronTrigger";
        private const string JOB_DETAIL_NAME = "SampleJobDetail";
        private readonly TextWriter textWriter;

        public WriterService()
        {
            InitializeComponent();
            string cronExpression = ConfigurationSettings.AppSettings["CronExpression"];
            CronTrigger cronTrigger = new CronTrigger(CRON_TRIGGER_NAME, null, cronExpression);
            JobDetail jobDetail = new JobDetail(JOB_DETAIL_NAME, null, typeof(EventLogJob));

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler();
            scheduler.ScheduleJob(jobDetail, cronTrigger);
            textWriter = new TextWriter();
        }

        protected override void OnStart(string[] args)
        {
            textWriter.Write("Service is Started");
            scheduler.Start();
        }

        protected override void OnStop()
        {
            textWriter.Write("Service is Stopped");
            scheduler.Shutdown(true);
        }
    }
}
