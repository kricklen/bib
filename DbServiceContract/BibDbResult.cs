#region Assembly Bib.DbServiceContract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Bib.DbServiceContract.dll
// Decompiled with ICSharpCode.Decompiler 7.2.1.6856
#endregion

using System;
using System.Data;

namespace Bib.DbServiceContract
{
    [Serializable]
    public class BibDbResult
    {
        public bool Success { get; set; }

        public DataSet DataSet { get; set; }

        public string ExMessage { get; set; }

        public string ExStacktrace { get; set; }
    }
}