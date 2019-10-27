using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LongRunningTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //TPL - Task based asynchrionous pattern
            var task0 = new SatData.LongRunningCpuProcessAsync().Process("task0", 120);

            // var salesRecordFile = @"C:\MyTestPrograms\1500000 Sales Records.csv";
            //var fileName = @"C:\MyTestPrograms\2012_sat_results.csv";

            var salesRecordFile = @".\Data\100 Sales Records.csv";
            var fileName = @".\Data\2012_sat_results.csv";

            Task<List<SatData.SatDao>> task1 = new SatData.SatQueryRepository(fileName).ReadFileAsync("task1");
            Task<List<SatData.SatDao>> task2 = new SatData.SatQueryRepository(fileName).ReadFileAsync("task2");
            Task<List<SatData.SatDao>> task3 = new SatData.SatQueryRepository(fileName).ReadFileAsync("task3");
            Task<List<SatData.SalesDao>> task4 = new SatData.SalesDataQueryRepository(salesRecordFile).ReadFileAsync("task4");
            Task.WaitAll(new Task[] {task0, task1,task4, task2,task3 });

            Console.WriteLine("task1 returned" + task1.Result.Count);
            Console.WriteLine("task4 returned" + task4.Result.Count);

            task4.Dispose();
            task3.Dispose();
            task2.Dispose();
            task1.Dispose();

            var data2 = new SatData.SatQueryRepository(fileName).ReadFileSync();
        }


    }
}
