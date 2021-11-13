using System;
using System.Collections.Generic;
using System.Linq;

namespace Question2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region via threads
            Thread thread1 = new Thread(() =>
            {
                DriveInfo driveInfo = new DriveInfo(@"C:\");
                DirectoryInfo directoryInfo = driveInfo.RootDirectory;
                var nameOfFirst10Files = directoryInfo.GetFiles().
                    Select(file => file.Name);
                foreach (var fileName in nameOfFirst10Files)
                {
                    Console.WriteLine(fileName);
                }
            });

            Thread thread2 = new Thread(() =>
            {
                DriveInfo driveInfo = new DriveInfo(@"D:\");
                DirectoryInfo directoryInfo = driveInfo.RootDirectory;
                var nameOfFirst10Files = directoryInfo.GetFiles().
                    Select(file => file.Name);
                foreach (var fileName in nameOfFirst10Files)
                {
                    Console.WriteLine(fileName);
                }
            });

            thread1.Start();
            thread2.Start();
            #endregion


            #region via task
            Task task = new Task(() =>
            {
                DriveInfo driveInfo = new DriveInfo(@"C:\");
                DirectoryInfo directoryInfo = driveInfo.RootDirectory;
                var nameOfFirst10Files = directoryInfo.GetFiles().
                    Select(file => file.Name);
                foreach (var fileName in nameOfFirst10Files)
                {
                    Console.WriteLine(fileName);
                }
                DriveInfo driveInfo1 = new DriveInfo(@"D:\");
                DirectoryInfo directoryInfo1 = driveInfo1.RootDirectory;
                var nameOfFirst10Files1 = directoryInfo1.GetFiles().
                    Select(file => file.Name);
                foreach (var fileName in nameOfFirst10Files1)
                {
                    Console.WriteLine(fileName);
                }
            };
            task.Start();
            #endregion
        }
    }
}
