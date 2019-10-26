using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LongRunningTest.SatData
{
    public class SalesDataQueryRepository
    {
        private string fileName = string.Empty;
        public SalesDataQueryRepository(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                this.fileName = fileName;
            }
            else
            {
                throw new NullReferenceException("Missing File Name :" + fileName);
            }
        }

        public async Task<List<SalesDao>> ReadFileAsync(string taskName)
        {
            Console.WriteLine("In Read File Async:" + taskName);
            var stringBuilder = new StringBuilder();
            var data = new List<SalesDao>();

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream))
            {
                string line = string.Empty;
                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    data.Add(SplitLine(line));
                }
            }
            Console.WriteLine("Exiting task:" + taskName);
            return data;
        }

        public SalesDao SplitLine(string data)
        {
            char[] delimiters = new char[] { ',' };
            string[] ss = data.ToString().Split(delimiters, 14);
            var salesData = new SalesDao
            {
                Region = ss[0],
                Country = ss[1],
                ItemType = ss[2],
                SalesChannel = ss[3],
                OrderPriority = ss[4],
                OrderDate = ss[5],
                OrderID = ss[6],
                ShipDate = ss[7],
                UnitsSold = ss[8],
                UnitPrice = ss[9],
                UnitCost = ss[10],
                TotalRevenue = ss[11],
                TotalCost = ss[12],
                TotalProfit = ss[13]
            };
            return salesData;
        }
    }
}
