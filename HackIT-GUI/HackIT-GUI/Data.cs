using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Internal;
using System.Globalization;
using System.Windows.Forms;


namespace HackIT_GUI
{
    class Data
    {
        public static string fetchRecord()
        {
            try
            {
                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("AKIAIKXGHXTQ5JWNVH2Q", "3TJrq+P+3jYObS8WtbpO6BRtB5e3QC9tC3nC8tm5");
                var client = new AmazonDynamoDBClient(awsCreden‌​tials, Amazon.RegionEndpoint.EUWest1);
                Table table = Table.LoadTable(client, "DeviceData");
                //var 

                DateTime twoWeeksAgoDate = DateTime.UtcNow - TimeSpan.FromDays(15);
                //RangeFilter filter = new RangeFilter(QueryOperator.GreaterThan, twoWeeksAgoDate);
                //Search search = table.Query("DynamoDB Thread 2", filter);
                return table;
               
            }
            catch (Exception e)
            {
                return null;
                MessageBox.Show(e.ToString());
            }

        }

        private static void FindRepliesPostedWithinTimePeriod(DynamoDBContext context, string forumName, string threadSubject)
        {
            string forumId = forumName + "#" + threadSubject;

            DateTime startDate = DateTime.UtcNow - TimeSpan.FromDays(30);
            DateTime endDate = DateTime.UtcNow - TimeSpan.FromDays(1);

           // IEnumerable<query> repliesInAPeriod = context.Query<Reply>(forumId,
           //                                QueryOperator.Between, startDate, endDate);
            //foreach (Reply r in repliesInAPeriod)
             //   Console.WriteLine("{0}\t{1}\t{2}\t{3}", r.Id, r.PostedBy, r.Message, r.ReplyDateTime);
        }

    }
}
