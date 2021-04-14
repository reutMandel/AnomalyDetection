using System;
using System.Net;
using System.Net.Sockets;

namespace AnomalyDetection.Model
{
    public class Client : IClient
    {
        private readonly string _ip;
        private readonly int _port;
        private Socket socket;

        public Client(string ip, int port)
        {
            _port = port;
            _ip = ip;
        }

        public void Connect()
        {
            try
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
                socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipEndPoint);
            }
            catch(Exception ex)
            {
                Disconnect();
                throw ex;
            }
        }

        public void Send(byte[] buffer)
        {
            socket.Send(buffer); 
        }

        public void Disconnect()
        {
            socket.Close();
        }
        
    }
}
