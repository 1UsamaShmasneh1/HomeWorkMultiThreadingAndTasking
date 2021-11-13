using System;
using System.Collections.Generic;
using System.Linq;

namespace Question3
{
    #region NumNum via threads
    class NumNum
    {
        public Thread thread;
        public int SleepTime {  get; set; }

        public NumNum(string threadName)
        {
            thread = new Thread(() => Sleep());
            SleepTime = new Random().Next(5);
            thread.Name = threadName;
            Console.WriteLine($"Thread '{thread.Name}'; Sleep time {SleepTime} seconds");
        }

        public void Sleep()
        {
            Console.WriteLine($"{thread.Name} going to sleep");
            Thread.Sleep(SleepTime);
            Console.WriteLine($"{thread.Name} done sleeping");

        }
    }
    #endregion


    #region NumNum via task
    class NumNum2
    {
        public Task task;
        public int SleepTime { get; set; }

        public NumNum2(string threadName)
        {
            task = new Task(() => Sleep());
            SleepTime = new Random().Next(5);
            Console.WriteLine($"Thread '{task.Id}'; Sleep time {SleepTime} seconds");
        }

        public void Sleep()
        {
            Console.WriteLine($"{task.Id} going to sleep");
            Thread.Sleep(SleepTime);
            Console.WriteLine($"{task.Id} done sleeping");
        }
    }
    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            NumNum numnum1 = new NumNum("numnum1");
            NumNum numnum2 = new NumNum("numnum2");
            NumNum numnum3 = new NumNum("numnum3");
            NumNum numnum4 = new NumNum("numnum4");
            Console.WriteLine("Starting threads");
            numnum1.thread.Start();
            numnum2.thread.Start();
            numnum3.thread.Start();
            numnum4.thread.Start();
            numnum1.thread.Join();
            numnum2.thread.Join();
            numnum3.thread.Join();
            numnum4.thread.Join();

            Console.WriteLine("Threads started");
        }
    }
}