using System;
using System.Collections.Generic;
using System.Text;

namespace LongRunningTest
{
    public class LongProcessSync
    {

        public string Run(string processName, int secondsToRun)
        {
            var startTime = DateTime.Now;
            var retVal = string.Empty;
            Console.WriteLine("Starting " + processName + "  at " + startTime.ToString());

            System.Threading.Thread.Sleep(secondsToRun * 1000);

            retVal = DateTime.Now.Subtract(startTime).TotalMinutes.ToString();
            Console.WriteLine("Ended " + processName + "  at " + DateTime.Now.ToString());
            return retVal;
        }

        public MyResult Run(string processName)
        {
            var startTime = DateTime.Now;
            var retVal = new MyResult();
            Console.WriteLine("Starting " + processName + "  at " + startTime.ToString());
            long processedVal = 0;
            for (var i=0; i< 100000; i++)
            {
                processedVal += i;
            }


            retVal.Result2 = DateTime.Now.Subtract(startTime).TotalMinutes.ToString();
            Console.WriteLine("Ended " + processName + "  at " + DateTime.Now.ToString());
            return retVal;
        }
    }
}
