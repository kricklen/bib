#region Assembly Bib.DbService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Bib.DbService.dll
// Decompiled with ICSharpCode.Decompiler 7.2.1.6856
#endregion

using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using Bib.DbServiceContract;

namespace Bib.DbService
{
    public class DbTcpClient
    {
        private static readonly BinaryFormatter _binaryFormatter;

        private readonly int _port;

        private readonly string _ip;

        static DbTcpClient()
        {
            _binaryFormatter = new BinaryFormatter();
        }

        public DbTcpClient(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public BibDbResult Query(BibDbCommand dbCommand)
        {
            var tcpClient = new TcpClient(_ip, _port);
            var stream = tcpClient.GetStream();
            try
            {
                WriteCommand(stream, dbCommand);
                return ReadResult(stream);
            }
            finally
            {
                stream.Close();
                tcpClient.Close();
            }
        }

        private static void WriteCommand(NetworkStream stream, BibDbCommand cmd)
        {
            _binaryFormatter.Serialize(stream, cmd);
        }

        private static BibDbResult ReadResult(NetworkStream stream)
        {
            return (BibDbResult)_binaryFormatter.Deserialize(stream);
        }
    }
}