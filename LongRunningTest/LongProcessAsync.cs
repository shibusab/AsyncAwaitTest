using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LongRunningTest
{
   public class LongProcessAsync
    {
        public async Task<string> Run(string processName, int secondsToRun)
        {
            var startTime = DateTime.Now;
            var retVal = string.Empty;
            Console.WriteLine("Starting " + processName + "  at " + startTime.ToString());

            await Task.Delay(secondsToRun * 1000);

            retVal = DateTime.Now.Subtract(startTime).TotalMinutes.ToString();
            Console.WriteLine("Ended " + processName + "  at " + DateTime.Now.ToString());
            return retVal;
        }

        //bad does not return value
        public Task<MyResult> Run(string processName)
        {
            return Task.Run(() => Process(processName));
        }

        private MyResult Process(string processName)
        {
            var retVal = new MyResult();
            var startTime = DateTime.Now;
            Console.WriteLine("Starting " + processName + "  at " + startTime.ToString());

            long processedVal = 0;
            for (var i = 0; i < 100000; i++)
            {
                processedVal += new  Random(1).Next();
            }
            retVal.Result2 = DateTime.Now.Subtract(startTime).TotalMinutes.ToString();

            Console.WriteLine("Ended " + processName + "  at " + DateTime.Now.ToString());

            return retVal;
        }
    }
}
