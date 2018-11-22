using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Reflection;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using System.Globalization;


namespace HackIT_simulator
{
    public class SimAccelerometer
    {
        public static bool run = true;
        public static bool running = false;

        public static string dataFile = "Multi.csv";
        public static string dataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dataFile);

        public static string deviceID = "2d887d72e0d4166540a33580358886e3";


        public void worker()
        {
            serverProcess();
        }

        public void serverProcess()
        {
            running = true;
            int tick = 0;

            Logging.WriteToLog("Accellerometer simulator running");

            using (var sr = new StreamReader(dataPath))
            {
                while (run == true)
                {
                    if (sr.EndOfStream)
                    {
                        sr.BaseStream.Position = 0;
                        sr.DiscardBufferedData();
                        Logging.WriteToLog("End of file, Returning to beginning");
                    }

                    var line = sr.ReadLine();
                    var values = line.Split(',');

                    // send data
                    SendRecord(values);

                    Thread.Sleep(6000);
                    tick++;
                }
            }

            Logging.WriteToLog("Accelerometer simulator stopped");
            running = false;
        }


        private static void SendRecord(string[] values)
        {
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                                        CultureInfo.InvariantCulture);

            try
            {
                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("AKIAIKXGHXTQ5JWNVH2Q", "3TJrq+P+3jYObS8WtbpO6BRtB5e3QC9tC3nC8tm5");
                var client = new AmazonDynamoDBClient(awsCreden‌​tials, Amazon.RegionEndpoint.EUWest1);
                var table = Table.LoadTable(client, "SensorData");
                //var 
                var item = new Document();


                item["TimeStamp"] = timestamp;
                item["DeviceID"] = deviceID;
                item["AccX"] = values[1];
                item["AccY"] = values[2];
                item["AccZ"] = values[3];

                table.PutItem(item);
                //Logging.WriteToLog(result.ToString());

            }
            catch (Exception e)
            {
                Logging.WriteToLog("Put request failed: " + e.ToString());
                Logging.WriteToLog("----------------------");
            }

        }
    }
}
