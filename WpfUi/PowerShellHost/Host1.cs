using System;
using System.Globalization;
using System.Management.Automation.Host;
using System.Windows.Documents;

namespace WpfUi.PowerShellHost
{
    public class Host1 : PSHost
    {
        public FlowDocument Flow { get; }

        public Host1(FlowDocument flow)
        {
            Flow = flow;
            this.InstanceId = Guid.NewGuid();
            this.HostUI = new PowerShellUI1(flow);
            this.UI = this.HostUI;
            this.Name = "Host1";
            this.Version = Version.Parse("0.1");
            CurrentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            CurrentUICulture = CurrentCulture;
        }

        public PowerShellUI1 HostUI { get; set; }

        public override void SetShouldExit(int exitCode)
        {
            UI.WriteDebugLine("exit");
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
            UI.WriteDebugLine("Begin application.");
        }

        public override void NotifyEndApplication()
        {
        }

        public override CultureInfo CurrentCulture { get; }
        public override CultureInfo CurrentUICulture { get; }
        public override Guid InstanceId { get; }
        public override string Name { get; }
        public override PSHostUserInterface UI { get; }
        public override Version Version { get; }

        public object UnderlyingUIObject
        {
            get { return HostUI.UnderlyingUIObject; }
            set { HostUI.UnderlyingUIObject = value; }
        }
    }
}