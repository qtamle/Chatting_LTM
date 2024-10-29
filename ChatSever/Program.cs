using ChatSever.Net.IO;
using System;
using System.Net;
using System.Net.Sockets;

namespace ChatSever
{
    class Program
    {
        static List<Client> _users;
        static TcpListener _listener;
        static void Main(string[] args)
        {
            _users = new List<Client>();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            _listener.Start();

            while (true)
            {
                var client = new Client(_listener.AcceptTcpClient());
                _users.Add(client);

                BroadcastConnection();
            }
            
        }

        static void BroadcastConnection()
        {
            foreach (var user in _users)
            {
                foreach (var usr in _users){
                    var boradcastPacket = new PacketBuilder();
                    boradcastPacket.WriteOpCode(1);
                    boradcastPacket.WriteMessage(usr.Username);
                    boradcastPacket.WriteMessage(usr.UID.ToString());
                    user.ClientSocket.Client.Send(boradcastPacket.GetPacketBytes());
                }
            }
        }

        public static void BroadcastMessage(string message)
        {
            foreach (var user in _users)
            {
                var msgPacket = new PacketBuilder();
                msgPacket.WriteOpCode(5);
                msgPacket.WriteMessage(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
        }

        public static void BroadcastDiconnected(string uid)
        {
            var disconnectedUser = _users.Where(x => x.UID.ToString() == uid).FirstOrDefault();
            _users.Remove(disconnectedUser);
            foreach (var user in _users)
            {
                var BroadcastPacket = new PacketBuilder();
                BroadcastPacket.WriteOpCode(10);
                BroadcastPacket.WriteMessage(uid);
                user.ClientSocket.Client.Send(BroadcastPacket.GetPacketBytes());
            }
            BroadcastMessage($"[{disconnectedUser.Username}]: Disconnected!");
        }
    }
}