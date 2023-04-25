#region Assembly Bib.DbService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Bib.DbService.dll
// Decompiled with ICSharpCode.Decompiler 7.2.1.6856
#endregion

using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using Bib.Common;
using Bib.DbServiceContract;

namespace Bib.DbService
{
    public class DbTcpService
    {
        private static readonly BinaryFormatter _binaryFormatter;

        private readonly ILogger _logger;

        private int _port;

        private string _dbConnectionString;


        private TcpListener _listener;

        static DbTcpService()
        {
            _binaryFormatter = new BinaryFormatter();
        }

        public DbTcpService()
        {
            _logger = new Logger("DbTcpService");
            //_logger = new FileLogger(@"C:\temp\DbTcpService_file.log");
            //File.AppendAllText(@"C:\temp\bib_test.log", "123");
            Init();
        }

        private void Init()
        {
            try
            {
                _port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                _dbConnectionString = ConfigurationManager.AppSettings["DbConnection"];
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Configuration Error");
            }
            _logger.Debug("Using Port {0}", _port);
            _logger.Debug("Using DbConnect {0}", _dbConnectionString);
            if (_port <= 0 || string.IsNullOrEmpty(_dbConnectionString))
            {
                throw new ArgumentException("Invalid Config Parameters");
            }
        }

        public void Start()
        {
            _listener = new TcpListener(IPAddress.Any, _port);
            _listener.Start();
            _listener.BeginAcceptTcpClient(AcceptClient, _listener);
        }

        private void AcceptClient(IAsyncResult ar)
        {
            try
            {
                var tcpClient = _listener.EndAcceptTcpClient(ar);
                _logger.Debug("--- Client Connected ---");
                var stream = tcpClient.GetStream();
                var cmd = ReadCommand(stream);
                WriteResult(stream, GetDbResult(cmd));
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, " SocketException: {0}");
            }
            _listener.BeginAcceptTcpClient(AcceptClient, null);
        }

        private BibDbCommand ReadCommand(NetworkStream stream)
        {
            var cmd = (BibDbCommand)_binaryFormatter.Deserialize(stream);
            _logger.Debug(" Query: {0}", cmd.Sql);
            return cmd;
        }

        private void WriteResult(NetworkStream stream, BibDbResult result)
        {
            _logger.Debug(" Returning DbResult");
            _binaryFormatter.Serialize(stream, result);
        }

        private BibDbResult GetDbResult(BibDbCommand cmd)
        {
            var result = new BibDbResult();
            try
            {
                var sqlBaseService = new SqlBaseService(_dbConnectionString);
                switch (cmd.Type)
                {
                    case BibDbCommandType.Select:
                        result.DataSet = sqlBaseService.Select(cmd.Sql);
                        break;
                    case BibDbCommandType.GetTables:
                        result.DataSet = sqlBaseService.GetTables();
                        break;
                    case BibDbCommandType.GetViews:
                        result.DataSet = sqlBaseService.GetViews();
                        break;
                }
                result.Success = true;
                _logger.Debug(" Query Success");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ExMessage = ex.Message;
                result.ExStacktrace = ex.StackTrace;
                _logger.Debug(" Query Error\n {0}", ex);
            }
            return result;
        }

        public void Stop()
        {
            try
            {
                _listener.Stop();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error stopping TcpListener");
            }
        }
    }
}