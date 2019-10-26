using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace LongRunningTest.SatData
{
    public class SatQueryRepository
    {
        private string fileName = string.Empty;
        public SatQueryRepository(string fileName)
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

        public async Task<List<SatDao>> ReadFileAsync(string taskName)
        {
            Console.WriteLine("In Read File Async:" + taskName);
            var stringBuilder = new StringBuilder();
            var data = new List<SatDao>();

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream))
            {
                string line = string.Empty ;
                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    data.Add(SplitLine(line));
                }
            }
            Console.WriteLine("Exiting task:" + taskName);
            return data;
        }

        public SatDao SplitLine(string data)
        {
            char[] delimiters = new char[] { ',' };
            string[] ss = data.ToString().Split(delimiters, 6);
              var satData=  new SatDao
                {
                    Division = ss[0],
                    SchoolName = ss[1],
                    NumofTestTakers = ss[2],
                    CriticalReadingAvgScore = ss[3],
                    MathAvgScore = ss[4],
                    WritingAvgScore = ss[5]
                };
            return satData;
        }
        public List<SatDao> ReadFileSync()
        {
            char[] delimiters = new char[] { ',' };
            var startLine = 1;

            var data = System.IO.File.ReadAllLines(fileName)
                                .Skip(startLine)
                                // .Take(lineCount)
                                .Select(line => {
                                    string[] ss = line.Split(delimiters, 6);
                                    return (ss.Length == 6)
                               ? new SatDao()
                               {
                                   Division = ss[0],
                                   SchoolName = ss[1],
                                   NumofTestTakers = ss[2],
                                   CriticalReadingAvgScore = ss[3],
                                   MathAvgScore = ss[4],
                                   WritingAvgScore = ss[5]
                               }
                               : null; // null for ill-formatted line
                    })
                                .Where(x => x != null) // filter to good lines
                                .ToList();
            return data;
        }
    }
}
