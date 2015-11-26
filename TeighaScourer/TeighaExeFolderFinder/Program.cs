using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TeighaExeFolderFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
            Find(dir);
        }

        private static void Find(string dir)
        {
            var di = new DirectoryInfo(dir);
            var matchedDirs = Find(di);
            foreach (var matchedDir in matchedDirs)
            {
                Console.WriteLine("TeighaScourer {0}", matchedDir.FullName);
            }
        }

        private static IEnumerable<DirectoryInfo> Find(DirectoryInfo dir)
        {
            if ((from file in dir.GetFiles()
                 where file.Extension.ToLower() == ".dll" select file.Name.ToLower())
                 .Any(fname => fname.StartsWith("td_alloc_")))
            {
                yield return dir;
            }

            foreach (var subdir in dir.GetDirectories())
            {
                var matchedDirs = Find(subdir);
                foreach (var matchedDir in matchedDirs)
                {
                    yield return matchedDir;
                }
            }
        }
    }
}
