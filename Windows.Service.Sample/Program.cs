using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace Windows.Service.Sample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun = new ServiceBase[] 
                                              { 
                                                  new WriterService() 
                                              };
            ServiceBase.Run(ServicesToRun);
            //QuartzService quartzService = new QuartzService();
            //quartzService.OnStart();
        }
    }
}
