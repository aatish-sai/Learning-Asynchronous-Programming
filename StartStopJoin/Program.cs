using System;
using System.Threading;

namespace StartStopJoin
{
    class Program
    {
        static void Main(string[] args)
        {
            Alpha a = new Alpha();

            Thread ta = new Thread(new ThreadStart(a.Beta));

            ta.Start();

            while (!ta.IsAlive)
            {

            }
            Thread.Sleep(1);

            ta.Abort();

            ta.Join();

            Console.WriteLine("finished");
            try
            {
                Console.WriteLine("restarting");
                ta.Start();
            }
            catch (ThreadStateException)
            {
                Console.WriteLine("failed");
            }
            return;
        }
    }

    public class Alpha
    {
        public void Beta()
        {
            while (true)
            {
                Console.WriteLine("running on its own thread");
            }
        }
    }
}
