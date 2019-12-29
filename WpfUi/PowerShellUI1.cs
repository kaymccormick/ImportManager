using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

namespace WpfUi
{
    public class PowerShellUI1 : PSHostUserInterface
    {
        public FlowDocument Flow { get; }

        public PowerShellUI1(FlowDocument flow)
        {
            Flow = flow ?? throw new ArgumentNullException(nameof(flow));
            WriteDebugLine("this is debug");
            errorQueue = new Queue<string>();
        }

        public override string ReadLine()
        {
            return "echo foo";
        }

        public override SecureString ReadLineAsSecureString()
        {
            throw new NotImplementedException();
        }

        public override void Write(string value)
        {
            WriteToFlow(value);
        }

        public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
        {
            WriteToFlow(value);
        }

        public override void WriteLine(string value)
        {
            WriteToFlow(value);
        }

        private void WriteToFlow(string value)
        {
            Action ac = delegate()
            {
                Paragraph p = new Paragraph();
                p.Inlines.Add(value);
                Flow.Blocks.Add(p);
            };
            Flow.Dispatcher.BeginInvoke(ac, DispatcherPriority.Normal);

        }


        public override void WriteErrorLine(string value)
        {
            WriteToFlow(value);

        }

        public Queue<String> errorQueue { get; set; }

        public override void WriteDebugLine(string message)
        {
            WriteToFlow(message);
        }

        public override void WriteProgress(long sourceId, ProgressRecord record)
        {
            throw new NotImplementedException();
        }

        public override void WriteVerboseLine(string message)
        {
            WriteToFlow(message);
        }

        public override void WriteWarningLine(string message)
        {
            WriteToFlow(message);
        }

        public override Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions)
        {
            Window promptWin = new Window();
            var promptWinContent = new Grid();
            //var row1 = new RowDefinition();
            //promptWinContent.RowDefinitions.Add();
            var uiElement = new Label();
            uiElement.Content = caption;
            var messageLabel = new Label();
            messageLabel.Content = message;
            promptWinContent.Children.Add(uiElement);
            promptWinContent.Children.Add(messageLabel);
            promptWin.Content = promptWinContent;
            foreach (var field in descriptions)
            {
                var label = new Label();
                label.Content = field.Label;
            }

            promptWin.ShowDialog();
            return null;
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName)
        {
            throw new NotImplementedException();
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName,
            PSCredentialTypes allowedCredentialTypes, PSCredentialUIOptions options)
        {
            throw new NotImplementedException();
        }

        public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, int defaultChoice)
        {
            throw new NotImplementedException();
        }

        public override PSHostRawUserInterface RawUI { get; }

        public object UnderlyingUIObject
        {
            get { return this.UnderlyingUIObject; }
            set
            {
                UnderlyingUIObject = value;
                DisplayControl = value as Panel;
            }
        }

        public Panel DisplayControl { get; set;}
    }
}
