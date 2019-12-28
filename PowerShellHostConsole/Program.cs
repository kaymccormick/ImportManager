using System;
using Microsoft.PowerShell.Commands;

namespace PowerShellHostConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PowerShellHost1 host = new PowerShellHost1();
            host.Initalize();
            var mainThread = host.StartMainThreadProc();
            var f = host.Factory;
            var queue = host.OperationQueue;
            queue.Add(f.CreateQuitOperation());
            mainThread.Join();
        }
    }
}
