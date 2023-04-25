using System;
using System.IO;

namespace Bib.Common
{
    public class FileLogger : ILogger
    {
        private readonly string fileName;

        public FileLogger(string fileName)
        {
            this.fileName = fileName;
        }

        private void makeLogLine(string level, string format, params object[] parameters)
        {
            var prefix = DateTime.Now.ToShortTimeString() + " [" + level + "]: ";
            File.AppendAllText(this.fileName, string.Format(prefix + format + "\n", parameters));
        }

        public void Debug(string format, params object[] parameters)
        {
            this.makeLogLine("Debug", format, parameters);
        }

        public void Error(Exception ex, string format, params object[] parameters)
        {
            this.makeLogLine("Error", format, parameters);
        }

        public void Info(string format, params object[] parameters)
        {
            this.makeLogLine("Info", format, parameters);
        }
    }
}
