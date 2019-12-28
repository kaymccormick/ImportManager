
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;
using Application = Microsoft.SqlServer.Dts.Runtime.Application;
using Package = Microsoft.SqlServer.Dts.Runtime.Package;
using TaskHost = Microsoft.SqlServer.Dts.Runtime.TaskHost;

namespace ImportManager
{
    public class PackageBuilder
    {
        public void BuildPackage()
        {
            Package package = new Package();
            String path =
                @"C:\Users\mccor.LAPTOP-T6T0BN1K\OneDrive\Documents\Account Data\LinkedIn\Complete_LinkedInDataExport_12-21-2019";
            DirectoryInfo info = new DirectoryInfo(path);
            foreach (FileInfo file in info.GetFiles())
            {
                Executable e = package.Executables.Add("STOCK:PipelineTask");
                TaskHost host = e as TaskHost;
                MainPipe dataflowTask = host.InnerObject as MainPipe;
                Microsoft.SqlServer.Dts.Runtime.Application app = new Application();

                foreach (PipelineComponentInfo x in app.PipelineComponentInfos)
                {
                    Console.WriteLine(x.Name + " " + x.CreationName);
                }
                var source = dataflowTask.ComponentMetaDataCollection.New();
                source.ComponentClassID = "DTSAdapter.FlatFileSource.7";
                var wrapper = source.Instantiate();
                wrapper.ProvideComponentProperties();
                var conn = package.Connections.Add("FLATFILE");
                conn.ConnectionString = file.FullName;
                if(source.RuntimeConnectionCollection.Count > 0)
                {
                    source.RuntimeConnectionCollection[0].ConnectionManagerID = conn.ID;
                    source.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(conn);
                }

                var dest = dataflowTask.ComponentMetaDataCollection.New();
                dest.ComponentClassID = "DTSAdapter.SQLServerDestination.7";//"{9A15CF9B-A48C-4FD8-BA0B-EF348C6A1967}";
                var destWrapper = dest.Instantiate();
                destWrapper.ProvideComponentProperties();
                var destConn = package.Connections.Add("ADO.NET");
                destConn.ConnectionString =
                    @"Data Source=.\sql2017;Initial Catalog=import;Provider=SQLNCLI11.1;Integrated Security=SSPI;Auto Translate=False;";
                if(dest.RuntimeConnectionCollection.Count > 0)
                {
                    dest.RuntimeConnectionCollection[0].ConnectionManagerID = destConn.ID;
                    dest.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(destConn);

                }
                //destWrapper.AcquireConnections();
                var path2 = dataflowTask.PathCollection.New();
                path2.AttachPathAndPropagateNotifications(source.OutputCollection[0], dest.InputCollection[0]);
                var vinput = dest.InputCollection[0].GetVirtualInput();
                foreach (IDTSVirtualInputColumn100 col in vinput.VirtualInputColumnCollection)
                {
                    destWrapper.SetUsageType(dest.InputCollection[0].ID, vinput, col.LineageID,
                        DTSUsageType.UT_READONLY);
                }

                break;
            }

            String packageXml = "";
            package.SaveToXML(out packageXml, new DefaultEvents());
            File.WriteAllText(@"c:\temp\out.dtsx", packageXml);
            package.Dispose();
        }
    }
}
