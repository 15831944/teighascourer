using System;
using System.Collections.Generic;
using System.IO;

namespace TeighaScourer
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
            var delCount = Scour(dir);
            Console.WriteLine("{0} file(s) deleted", delCount);
        }

        private static int Scour(string dir)
        {
            var delCount = 0;
            var dirInfo = new DirectoryInfo(dir);
            var fns = new HashSet<string>();
            foreach (var f in dirInfo.GetFiles())
            {
                var ext = f.Extension.ToLower();
                if (ext == ".dll" || ext == ".tx" || ext == ".txv")
                {
                    var fn = Path.GetFileNameWithoutExtension(f.Name.ToLower());
                    fns.Add(fn);
                }
            }
            foreach (var f in dirInfo.GetFiles())
            {
                var ext = f.Extension.ToLower();
                if (ext == ".dll" || ext == ".tx" || ext == ".txv")
                {
                    continue;
                }
                if (ext == ".pdb")
                {
                    var fn = Path.GetFileNameWithoutExtension(f.Name.ToLower());
                    if (fns.Contains(fn))
                    {
                        continue;
                    }
                }
                var fname = f.Name.ToLower();
                var processName = AppDomain.CurrentDomain.FriendlyName.ToLower();
                if (fname == processName)
                {
                    continue;
                }
                f.Delete();
                delCount++;
            }
            return delCount;
        }
    }
}
