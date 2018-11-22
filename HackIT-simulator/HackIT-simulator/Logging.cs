using System;
using System.IO;

namespace HackIT_simulator
{
    public class Logging
    {
        private static string LogFolder = "C:\\HackIT";
        private static string LogPath = Path.Combine(LogFolder, "HackITLog.txt");

        public static void WriteToLog(string msg)
        {
            if (!Directory.Exists(LogFolder))
            {
                Directory.CreateDirectory(LogFolder);
            }

            using (StreamWriter sw = new StreamWriter(LogPath, true))
            {
                sw.WriteLine(DateTime.Now.ToString() + ": " + msg);
            }
        }
    }
}
