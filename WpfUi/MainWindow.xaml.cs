using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfUi.PowerShellHost;

namespace WpfUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        public void InitPowerShell()
        {
            Host1 host = new Host1(FlowDocument);
            runspace = RunspaceFactory.CreateRunspace(host);
            runspace.Open();
            powershell = PowerShell.Create();
            powershell.Runspace = runspace;
            powershell.AddScript(@"
                        get-process | sort handlecount
                        # This should display the date in German...
                        get-date | out-string
                        ");

            // Now add the default outputter to the end of the pipe and indicate
            // that it should handle both output and errors from the previous
            // commands. This will result in the output being written using the PSHost
            // and PSHostUserInterface classes instead of returning objects to the hosting
            // application.
            powershell.AddCommand("out-default");

            powershell.Commands.Commands[0].MergeMyResults(PipelineResultTypes.Error, PipelineResultTypes.Output);

            // Now just invoke the application - there won't be any objects returned -
            // they're all consumed by out-default so we don't have to do anything more...
            powershell.BeginInvoke();
            // powershell.AddCommand("Get-Module");
            // powershell.AddCommand("out-default");
            // powershell.Commands.Commands[0].MergeMyResults(PipelineResultTypes.Error, PipelineResultTypes.Output);
            // powershell.Invoke();
            // //this.Properties["PowerShellInstance"] = powershell;
            //this.Properties["PSHostInstance"] = host;

        }

        public PowerShell powershell { get; set; }

        public Runspace runspace { get; set; }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.InitPowerShell();
        }
    }
}
