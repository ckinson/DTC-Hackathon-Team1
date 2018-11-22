using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using tDB;
using Update;
using CommSvr;

namespace Roots
{
    public class Background
    {
        public static bool run = true;
        public static bool running = false;

        public static int uptime = 0;

        private static int tick = 0;
        private static int countA = 0;
        private static int countB = 0;
        private static int countC = 0;
        private static int countD = 0;

        public void worker()
        {
            timedSVCS();
        }

        public void timedSVCS()
        {
            running = true;

            uptime = int.Parse(tDB.Settings.ReadSetting("Uptime"));

            while (run == true)
            {
                try
                {
                    if (countA > 30)
                    {
                        tDB.Settings.WriteSetting("Uptime", (uptime + tick).ToString());
                        countA = 0;
                    }
                    if (countB > 3600)
                    {
                        Updater.CheckUpdates();
                        countB = 0;
                    }
                    if (countC > 60)
                    {
                        Discovery.Poll();
                        countC = 0;
                    }
                    if (countD > 120)
                    {
                        LogUploader.Uploader();
                        countD = 0;
                    }
                    //
                }
                catch //(Exception e)
                {
                    //Logging.WriteToLog(0, "CommSvr", e.ToString());
                }
                Thread.Sleep(1000);
                tick++;
                countA++;
                countB++;
                countC++;
                countD++;
            }
            tDB.Settings.WriteSetting("Uptime", (uptime + tick).ToString());
    
            Logging.WriteToLog(0, "Roots service", "Uptime: " + tick);
            Logging.WriteToLog(0, "Roots service", "Total uptime: " + tDB.Settings.ReadSetting("Uptime"));
            running = false;
        }


    }
}
