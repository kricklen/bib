#region Assembly Bib.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Bib.Common.dll
// Decompiled with ICSharpCode.Decompiler 7.2.1.6856
#endregion

using System;

namespace Bib.Common
{
    public interface ILogger
    {
        void Debug(string format, params object[] parameters);

        void Info(string format, params object[] parameters);

        void Error(Exception ex, string format, params object[] parameters);
    }
}