using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportManager;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            PackageBuilder packageBuilder = new PackageBuilder();
            packageBuilder.BuildPackage();
        }

    }
}
