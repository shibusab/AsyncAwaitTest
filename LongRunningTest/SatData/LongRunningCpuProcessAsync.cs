using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace LongRunningTest.SatData
{
    public class LongRunningCpuProcessAsync
    {
        public async System.Threading.Tasks.Task<SatDao> Process(string taskName, int seconds)
        {
            Console.WriteLine("In task:" + taskName);
            var item = new SatDao();
            await System.Threading.Tasks.Task.Delay(seconds* 1000);

            Console.WriteLine("Exiting task:" + taskName);
            return item;
        }
    }
}
