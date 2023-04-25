#region Assembly Bib.DbServiceContract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Bib.DbServiceContract.dll
// Decompiled with ICSharpCode.Decompiler 7.2.1.6856
#endregion

using System;

namespace Bib.DbServiceContract
{
    [Serializable]
    public class BibDbCommand
    {
        public BibDbCommandType Type { get; set; }

        public string Sql { get; set; }

        public BibDbCommand()
        {
            Type = BibDbCommandType.Select;
        }
    }
}