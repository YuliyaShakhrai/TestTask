using System;

namespace DualLab_Shakhrai
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new TxtService();
            string filepath = "Schedule.txt";

            var schedule = service.LoadData(filepath);

            schedule.Optimize();

            string unload = "Result.txt";

            service.UnloadData(unload, schedule);

            Console.ReadLine();
        }
    }
}
