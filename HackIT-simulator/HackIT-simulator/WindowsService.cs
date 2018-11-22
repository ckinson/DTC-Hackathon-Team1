using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.IO;

namespace HackIT_simulator
{
    class WindowsService : ServiceBase
    {
        /// Public Constructor for WindowsService.
        public WindowsService()
        {
            this.ServiceName = "HackIT_simulator";
            this.EventLog.Log = "Application";

            // These Flags set whether or not to handle that specific
            //  type of event. Set to true if you need it, false otherwise.
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
        }

        /// <summary>
        /// The Main Thread: This is where your Service is Run.
        /// </summary>
        static void Main()
        {
            ServiceBase.Run(new WindowsService());
        }

        /// Dispose of objects that need it here.
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// OnStart(): Put startup code here
        protected override void OnStart(string[] args)
        {
            Logging.WriteToLog("Service start");

            Simulator svr = new Simulator();
            Thread svrThread = new Thread(svr.worker);
            svrThread.Start();

            //Simulator acc = new Simulator();
            //Thread accThread = new Thread(acc.worker);
            //accThread.Start();

            base.OnStart(args);
        }

        private void StopAll()
        {
            Simulator.run = false;
            SimAccelerometer.run = false;

            while (Simulator.running == true)
            {
                Thread.Sleep(500);
            }

            //while (SimAccelerometer.running == true)
            //{
            //    Thread.Sleep(500);
            //}


            Logging.WriteToLog("Service stopped");
            Logging.WriteToLog("................");
        }

        protected override void OnStop()
        {
            StopAll();

            Thread.Sleep(1000);
            base.OnStop();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnContinue()
        {
            base.OnContinue();
        }

        protected override void OnShutdown()
        {
            Logging.WriteToLog("Shutdown requested");

            StopAll();
            base.OnShutdown();
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }
    }
}