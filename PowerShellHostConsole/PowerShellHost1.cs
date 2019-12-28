    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Management.Automation;
    using System.Management.Automation.Host;
    using System.Management.Automation.Runspaces;
    using System.Text;
    using System.Threading;

    namespace PowerShellHostConsole
{
    class PowerShellHost1
    {
        class Host1 : PSHost
        {
            public Host1()
            {
                this.InstanceId = Guid.NewGuid();
            }

            public override void EnterNestedPrompt()
            {
                throw new NotImplementedException();
            }

            public override void ExitNestedPrompt()
            {
                throw new NotImplementedException();
            }

            public override void NotifyBeginApplication()
            {
                throw new NotImplementedException();
            }

            public override void NotifyEndApplication()
            {
                throw new NotImplementedException();
            }

            public override void SetShouldExit(int exitCode)
            {
                throw new NotImplementedException();
            }

            public override CultureInfo CurrentCulture { get; }
            public override CultureInfo CurrentUICulture { get; }
            public override Guid InstanceId { get; }
            public override string Name { get; }
            public override PSHostUserInterface UI { get; }
            public override Version Version { get; }
        }
        public class Operation
        {
            private DateTime creationTime;

            public Operation()
            {
                this.creationTime = DateTime.Now;
            }
        }

        public class OperationFactory
        {
            public Operation CreateQuitOperation()
            {
                return new QuitOperation();
            }
        }

        class QuitOperation : Operation
        {

        }

        public BlockingCollection<Operation> OperationQueue { get; } = new BlockingCollection<Operation>();
        private Runspace runspace = null;
        private Thread thread;

        public void Initalize()
        {
            this.createConstrainedRunspace();
            this.runspace.Open();
            this.powershell = PowerShell.Create();
            this.Factory = new OperationFactory();


        }

        public OperationFactory Factory { get; set; }

        public Thread StartMainThreadProc()
        {
            this.thread = new Thread(this.mainThreadProc);
            this.thread.Start();
            return thread;
        }

        private void mainThreadProc(object? obj)
        {
            while (!this.OperationQueue.IsCompleted)
            {
                Operation op = this.OperationQueue.Take();
                if(op is QuitOperation)
                {
                    break;
                }


            }

        }

        public PowerShell powershell { get; set; }

        public Runspace createConstrainedRunspace()
        {
            InitialSessionState iss = InitialSessionState.Create();
            this.runspace = RunspaceFactory.CreateRunspace(iss);
            return this.runspace;
        }


       // delegate Runspace createRunspaceDelegate();
        //private createRunspaceDelegate createRunSpace = createConstrainedRunspace;
    }
}
