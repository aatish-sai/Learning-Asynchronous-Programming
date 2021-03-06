﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            var demo = new AsyncAwait();
            demo.DoWork();

            while (true)
            {
                Console.WriteLine("Doing some work");
            }
        }
    }

    public class AsyncAwait
    {
        public async Task DoWork()
        {
            await Task.Run(() =>
            {
                LongTask();
            });
        }

        private static async Task<string> LongTask()
        {
            int counter;

            for(counter = 0; counter < 50000; counter++)
            {
                Console.WriteLine(counter);
            }

            return "counter = " + counter;
        }
    }
}
