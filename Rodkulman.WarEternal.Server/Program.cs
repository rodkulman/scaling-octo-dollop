using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Rodkulman.WarEternal.Server
{
    class Program
    {
        private static readonly Dictionary<string, Character> onlineCharacters = new Dictionary<string, Character>();;

        static async void Main(string[] args)
        {
            var world = new World();

            world.LoadRooms();

            var server = new TcpListener(IPAddress.Any, Settings.Port);

            while (true)
            {
                var socket = await server.AcceptSocketAsync();

                var handler = new SocketHandler(socket);

                var message = await handler.ReadMessage();

                switch (message.Value<string>("type"))
                {
                    case "login":
                        Login(message.Value<string>("character"));
                        break;
                    default:
                        break;
                }
            }
        }

        private static void Login(string character)
        {
            
        }
    }
}
