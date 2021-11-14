using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Question5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Search1 search1 = new Search1("D", "c");
            var resul = search1.Searching();
        }
    }


    #region Search via threads
    class Search1
    {
        public string Drive {  get; set; }
        public string SearchTerm {  get; set; }
        public List<string> Extensions { get; set; } = new List<string>() { "txt" };

        public Search1(string drive, string searchTerm)
        {
            Drive = drive;
            SearchTerm = searchTerm;
        }

        public List<string> Searching()
        {
            List<string> results = new List<string>();

            var Files = GetFiles();

           
            return results;
        }

        private List<dynamic> GetFiles()
        {
            DriveInfo driveInfo = new DriveInfo($@"{Drive}:\");
            DirectoryInfo directoryInfo = driveInfo.RootDirectory;
            var AllFiles = new List<dynamic>();
            object lockObject = new object();
            var relevantFiles = new List<dynamic>();
            AllFiles.AddRange(directoryInfo.GetFiles()
                .Where(file => IsMatchExtensions(file.Name.ToString())));
            foreach (var file in AllFiles)
            {
                new Thread(() =>
                {
                    if (IsContainsSearchTerm(file.ToString()))
                    {
                        lock (lockObject)
                        {
                            relevantFiles.Add(file);
                        }
                    }
                }).Start();
            }
            return relevantFiles;
        }

        private bool IsContainsSearchTerm(string file)
        {
            
            string line;
            using (StreamReader  reader = new StreamReader(file))
            {
                while((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(SearchTerm))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsMatchExtensions(string extension)
        {
            Regex regex = new Regex(@"^*\.txt$");
            return regex.IsMatch(extension);
        }
    }
    #endregion


    #region Search via task
    class Search2
    {
        public string Drive { get; set; }
        public string SearchTerm { get; set; }
        public List<string> Extensions { get; set; } = new List<string>() { "txt" };

        public Search2(string drive, string searchTerm)
        {
            Drive = drive;
            SearchTerm = searchTerm;
        }

        public List<string> Searching()
        {
            List<string> results = new List<string>();

            var Files = GetFiles();


            return results;
        }

        private List<dynamic> GetFiles()
        {
            DriveInfo driveInfo = new DriveInfo($@"{Drive}:\");
            DirectoryInfo directoryInfo = driveInfo.RootDirectory;
            var AllFiles = new List<dynamic>();
            object lockObject = new object();
            var relevantFiles = new List<dynamic>();
            AllFiles.AddRange(directoryInfo.GetFiles()
                .Where(file => IsMatchExtensions(file.Name.ToString())
                && IsContainsSearchTerm(file.Directory.ToString()
                + file.Name.ToString())).ToList<dynamic>());
            Task.Run(() => 
            {
                foreach (var file in AllFiles)
                {
                    if (IsContainsSearchTerm(file.Name.ToString()))
                    {
                        lock (lockObject)
                        {
                            relevantFiles.Add(file);
                        }
                    }
                }
            });
            return relevantFiles;
        }

        private bool IsContainsSearchTerm(string file)
        {
            string line;
            using (StreamReader reader = new StreamReader(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(SearchTerm))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsMatchExtensions(string extension)
        {
            Regex regex = new Regex(@"^*\.txt$");
            return regex.IsMatch(extension);
        }
    }
    #endregion

}