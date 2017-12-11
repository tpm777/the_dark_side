using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WWTS.ServiceApp;

namespace WWTS.BackgroundJob
{
    public partial class Service2 : System.ServiceProcess.ServiceBase
    {
        private AsyncBootstrapper asyncBootstrapper;
        public Service2()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //#if DEBUG
            //System.Diagnostics.Debugger.Launch();
            //#endif
            this.asyncBootstrapper = new ServiceApp.AsyncBootstrapper();
            this.OnStop();
        //Me.asyncBootstrapper.OnStopped = True


            this.asyncBootstrapper.StartAsync();
            ServiceBootstrapper.Initialize();
        }

        protected override void OnStop()
        {
            this.asyncBootstrapper.Dispose();
        }
    }
}
