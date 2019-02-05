using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rodkulman.WarEternal.Server
{
    public class World
    {
        private Dictionary<string, Room> rooms;

        public World()
        {
            rooms = new Dictionary<string, Room>();
        }

        public void LoadRooms()
        {
            JObject worldFile;

            using (var file = File.OpenRead("world.json"))
            using (var textReader = new StreamReader(file))
            using (var reader = new JsonTextReader(textReader))
            {
                worldFile = JObject.Load(reader);
            }

            foreach (var roomFile in worldFile["rooms"])
            {
                rooms.Add(roomFile.Value<string>("id"), new Room()
                {
                    Id = roomFile.Value<string>("id"),
                    Name = roomFile.Value<string>("name")
                });
            }

            foreach (var roomFile in worldFile["rooms"])
            {
                rooms[roomFile.Value<string>("id")].Borders = new Borders()
                {
                    North = rooms.GetValueOrDefault(roomFile["borders"].Value<string>("north")),
                    South = rooms.GetValueOrDefault(roomFile["borders"].Value<string>("south")),
                    East = rooms.GetValueOrDefault(roomFile["borders"].Value<string>("east")),
                    West = rooms.GetValueOrDefault(roomFile["borders"].Value<string>("west"))
                };
            }
        }
    }
}
