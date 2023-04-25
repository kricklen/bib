#region Assembly Bib.DbServiceHost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Bib.DbServiceHost.exe
// Decompiled with ICSharpCode.Decompiler 7.2.1.6856
#endregion

using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Bib.DbServiceHost
{
    [RunInstaller(true)]
    public class InternalServiceInstaller : Installer
    {
        public InternalServiceInstaller()
        {
            var val = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem,
                Username = null,
                Password = null
            };
            var val2 = new ServiceInstaller
            {
                DisplayName = "Bib DB Service",
                StartType = ServiceStartMode.Automatic,
                ServiceName = "Bib DB Service"
            };
            this.Installers.Add(val);
            this.Installers.Add(val2);
        }
    }
}