#region Assembly Bib.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Bib.Common.dll
// Decompiled with ICSharpCode.Decompiler 7.2.1.6856
#endregion

using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace Bib.Common
{
    public class Logger : ILogger
    {
        private readonly ILog _logger;

        public Logger(string prefix)
        {
            var baseDir = Assembly.GetExecutingAssembly().Location;
            XmlConfigurator.Configure(File.OpenRead(Path.Combine(Path.GetDirectoryName(baseDir), "log4net.config")));
            _logger = LogManager.GetLogger(prefix);
        }

        public void Debug(string format, params object[] parameters)
        {
            _logger.DebugFormat(format, parameters);
        }

        public void Info(string format, params object[] parameters)
        {
            _logger.InfoFormat(format, parameters);
        }

        public void Error(Exception ex, string format, params object[] parameters)
        {
            _logger.Error((object)string.Format(format, parameters), ex);
        }
    }
}