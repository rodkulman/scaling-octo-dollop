using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rodkulman.WarEternal.Server
{
    public static class Settings
    {
        private static JObject settingsFile;

        private static void LoadFile()
        {
            if (settingsFile != null) { return; }

            using (var file = File.OpenRead("settings.json"))
            using (var textReader = new StreamReader(file))
            using (var reader = new JsonTextReader(textReader))
            {
                settingsFile = JObject.Load(reader);
            }
        }

        public static int Port
        {
            get
            {
                LoadFile();

                return settingsFile.Value<int>("Port");
            }
        }

        public static string CriptographyKey
        {
            get
            {
                LoadFile();

                return settingsFile.Value<string>("CriptographyKey");
            }
        }
    }
}
