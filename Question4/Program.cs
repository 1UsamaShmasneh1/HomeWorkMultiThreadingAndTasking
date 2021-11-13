using System;
using System.Collections.Generic;
using System.Linq;

namespace Question4
{
    public class Program
    {
        public static void Main(string[] args)
        {

        }
    }

    #region Sum via thread
    class Sum1
    {
        public int TotalSum {  get; set; }
        object lockObject = new object();

        public Sum1(int num)
        {
            for(int i = 0; i < num / 200000; i++)
            {
                new Thread(() => SumNumbers(i * 200000, (i + 1) * 200000)).Start();

            }
            new Thread(() => SumNumbers(num - (num % 200000), num)).Start();
        }

        private void SumNumbers(int num1, int num2)
        {
            for(int i = num1; i <= num2; i++)
            {
                lock(lockObject)
                {
                    TotalSum += i;
                }
            }
        }
    }
    #endregion


    #region Sum via task
    class Sum2
    {
        public int TotalSum { get; set; }
        object lockObject = new object();

        public Sum2(int num)
        {
            Task.Run(() => 
            {
                for(int i = 0; i <= num; i++)
                {
                    lock (lockObject)
                    {
                        TotalSum += i;
                    }
                }
            });
        }
    }
    #endregion    
}