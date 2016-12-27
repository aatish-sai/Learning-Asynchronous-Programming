using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = new Task(ProcessAsync);
            task.Start();
            task.Wait();
            Console.ReadLine();
        }

        static async void ProcessAsync()
        {
            Task<int> task = HandleAsync("C:\\test.txt");

            Console.WriteLine("Please wait while I process data");

            int x = await task;
            Console.WriteLine("Count: " + x);
        }

        static async Task<int> HandleAsync(string file)
        {
            Console.WriteLine("Handler entered");
            int i = 0;
            using(StreamReader reader = new StreamReader(file))
            {
                string v = await reader.ReadToEndAsync();
                //do some file precessing here
                i += v.Length;
                for(int count = 0; count < 100000; count++)
                {
                    int x = v.GetHashCode();
                    if(x == 0)
                    {
                        count--;
                    }
                }
            }
            Console.WriteLine("Handler Exited");
            return i;
        }
    }
}
