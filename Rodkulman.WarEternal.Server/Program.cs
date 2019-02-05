using System;

namespace Rodkulman.WarEternal.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new World();

            world.LoadRooms();
        }
    }
}
