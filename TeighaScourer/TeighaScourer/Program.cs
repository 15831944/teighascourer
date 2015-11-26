using System.Collections.Generic;
using System.IO;

namespace TeighaScourer
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir;
            if (args.Length > 0)
            {
                dir = args[0];
            }
            else
            {
                dir = Directory.GetCurrentDirectory();
            }
            Scour(dir);
        }

        private static void Scour(string dir)
        {
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
                f.Delete();
            }
        }
    }
}
