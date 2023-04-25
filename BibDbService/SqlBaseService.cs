#region Assembly Bib.DbService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Bib.DbService.dll
// Decompiled with ICSharpCode.Decompiler 7.2.1.6856
#endregion

using System.Data;
using Bib.Common;
using Gupta.SQLBase.Data;

namespace Bib.DbService
{
    public class SqlBaseService
    {
        private readonly ILogger _logger;
        private readonly string _connectionString;

        public SqlBaseService(string connectionString)
        {
            _connectionString = connectionString;
            _logger = new Logger("SqlBaseService");
        }

        public DataSet Select(string sql)
        {
            var connection = new SQLBaseConnection(_connectionString);
            _logger.Debug("Opening connection...");
            try
            {
                connection.Open();

                //var cmd = new SQLBaseCommand(sql, connection);
                var adapter = new SQLBaseDataAdapter(sql, connection);
                try
                {
                    var dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    return dataSet;
                }
                finally
                {
                    adapter?.Dispose();
                }
            }
            finally
            {
                connection?.Dispose();
            }
        }

        public DataSet GetTables()
        {
            var connection = new SQLBaseConnection(_connectionString);
            _logger.Debug("Opening connection...");
            try
            {
                connection.Open();
                var table = connection.GetSQLBaseSchemaTable(SQLBaseSchemaGuid.Tables, null);
                var dataSet = new DataSet();
                dataSet.Tables.Add(table);
                return dataSet;
            }
            finally
            {
                connection?.Dispose();
            }
        }

        public DataSet GetViews()
        {
            var connection = new SQLBaseConnection(_connectionString);
            try
            {
                connection.Open();
                var table = connection.GetSQLBaseSchemaTable(SQLBaseSchemaGuid.Views, null);
                var dataSet = new DataSet();
                dataSet.Tables.Add(table);
                return dataSet;
            }
            finally
            {
                connection?.Dispose();
            }
        }
    }
}