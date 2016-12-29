using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MonitorSample
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = 0;
            Cell cell = new Cell();

            Producer prod = new Producer(cell, 20);
            Consumer cons = new Consumer(cell, 20);

            Thread producer = new Thread(new ThreadStart(prod.ThreadRun));
            Thread consumer = new Thread(new ThreadStart(cons.ThreadRun));

            try
            {
                producer.Start();
                consumer.Start();

                producer.Join();
                consumer.Join();

            }
            catch(ThreadInterruptedException e)
            {
                Console.WriteLine(e);
                result = 1;
            }
            catch(ThreadStateException e)
            {
                Console.WriteLine(e);
                result = 1;
            }
            Environment.ExitCode = result;
            
        }
    }

    public class Cell
    {
        int content;
        bool readFlag = false;
        public int ReadFromCell()
        {
            lock (this)
            {
                if (!readFlag)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch(SynchronizationLockException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch(ThreadInterruptedException e)
                    {
                        Console.WriteLine(e);
                    }
                }
                Console.WriteLine("Consume: {0}", content);
                readFlag = false;

                Monitor.Pulse(this);
            }
            return content;
        }

        public void WritetoCell(int n)
        {
            lock (this)
            {
                if (readFlag)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch(SynchronizationLockException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch(ThreadInterruptedException e)
                    {
                        Console.WriteLine(e);
                    }
                }
                content = n;
                Console.WriteLine("produce: {0}", content);
                readFlag = true;

                Monitor.Pulse(this);
            }
        }
    }

    public class Producer
    {
        Cell cell;
        int quantity = 1;

        public Producer(Cell c, int n)
        {
            cell = c;
            quantity = n;
        }
        public void ThreadRun()
        {
            for(int i = 1; i <= quantity; i++)
            {
                cell.WritetoCell(i);
            }
        }
    }

    public class Consumer
    {
        Cell cell;
        int quantity = 1;

        public Consumer(Cell c,int n)
        {
            cell = c;
            quantity = n;
        }
        public void ThreadRun()
        {
            int returnvalue;
            for(int i = 1; i <= quantity; i++)
            {
                returnvalue = cell.ReadFromCell();
            }
        }
    }
}
