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
    public class Simulator
    {
        public static bool run = true;
        public static bool running = false;

        public static string dataFile = "CombinedData.csv";
        public static string dataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dataFile);

        public static string deviceID = "6d4187d72a058035886e402d886533e3";


        public void worker()
        {
            serverProcess();
        }

        public void serverProcess()
        {
            running = true;
            int tick = 0;

            Logging.WriteToLog("Simulator running");

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

                    // process inport
                    var line = sr.ReadLine();
                    var values = line.Split(',');

                    // send data
                    SendRecord(values);

                    // pause between records (defined as frequency of supplied data)
                    Thread.Sleep(60000);
                    tick++;
                }
            }

            Logging.WriteToLog("Simulator stopped");
            running = false;
        }


        private static void SendRecord(string[] values)
        {
            long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            try
            {
                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("AKIAIKXGHXTQ5JWNVH2Q", "3TJrq+P+3jYObS8WtbpO6BRtB5e3QC9tC3nC8tm5");
                var client = new AmazonDynamoDBClient(awsCreden‌​tials, Amazon.RegionEndpoint.EUWest1);
                var table = Table.LoadTable(client, "DeviceData");
                //var 
                var item = new Document();


                item["TimeStamp"] = timestamp;
                item["DeviceID"] = deviceID;
                item["HeartRate"] = values[2];
                item["BodyTemp"] = values[3];
                item["Lux"] = values[4];
                item["RoomTemp"] = values[5];

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
