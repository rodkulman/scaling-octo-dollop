using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rodkulman.WarEternal.Server
{
    public class SocketHandler
    {
        private readonly Socket socket;

        public SocketHandler(Socket socket)
        {
            this.socket = socket;
        }

        public async Task<JObject> ReadMessage()
        {
            using (var mem = new MemoryStream())
            using (var cypher = new RijndaelManaged())
            {
                var buffer = new Memory<byte>();
                int read;

                while ((read = await socket.ReceiveAsync(buffer, SocketFlags.None)) > 0)
                {
                    await mem.WriteAsync(buffer);
                }

                using (var cryptoStream = new CryptoStream(mem, cypher.CreateDecryptor(Settings.CriptographyKey, Settings.CriptographyIV), CryptoStreamMode.Read))
                using (var textReader = new StreamReader(cryptoStream, Encoding.UTF8))
                using (var reader = new JsonTextReader(textReader))
                {
                    return await JObject.LoadAsync(reader);
                }
            }
        }
    }
}
