using System;
using System.Collections.Generic;
using System.Linq;

namespace Question1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region via thread
            Thread thread = new Thread(() =>
            {
                for (int i = 1; i < 5000; i++)
                {
                    Console.WriteLine(i);
                }
            });
            thread.Start();
            #endregion

            #region via task
            Task task = new Task(() =>
            {
                for (int i = 1; i < 5000; i++)
                {
                    Console.WriteLine(i);
                }
            });
            task.Start();
            #endregion
        }
    }
}

