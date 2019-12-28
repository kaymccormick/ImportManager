using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;

namespace PowerShellHostConsole
{
    class NavigationProvider : NavigationCmdletProvider
    {
        protected override bool IsValidPath(string path)
        {
            throw new NotImplementedException();
        }

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {

            return base.InitializeDefaultDrives();
        }

        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            return base.NewDrive(drive);
        }

        protected override object NewDriveDynamicParameters()
        {
            return base.NewDriveDynamicParameters();
        }

        protected override ProviderInfo Start(ProviderInfo providerInfo)
        {
            return base.Start(providerInfo);
        }
    }
}
