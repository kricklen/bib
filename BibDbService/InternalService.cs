#region Assembly Bib.DbServiceHost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Bib.DbServiceHost.exe
// Decompiled with ICSharpCode.Decompiler 7.2.1.6856
#endregion

using System;
using System.Diagnostics;
using System.ServiceProcess;
using Bib.DbService;

namespace Bib.DbServiceHost
{
    public class InternalService : ServiceBase
    {
        private DbTcpService _dbTcpService;

        public InternalService()
        {
            this.ServiceName = "Bib DB Service";
            this.EventLog.Log = "Application";
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = false;
            this.CanPauseAndContinue = false;
            this.CanShutdown = true;
            this.CanStop = true;
        }

        private static void Main()
        {
            ServiceBase.Run(new InternalService());
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            try
            {
                _dbTcpService = new DbTcpService();
                _dbTcpService.Start();
            }
            catch (Exception ex)
            {
                this.EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
                this.EventLog.WriteEntry(ex.StackTrace, EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            _dbTcpService?.Stop();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
            _dbTcpService?.Stop();
        }
    }
}