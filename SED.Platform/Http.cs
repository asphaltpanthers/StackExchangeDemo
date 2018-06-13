using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SED.Platform
{
    public class Http
    {
        private HttpClient _http;

        public Http(string baseAddress)
        {
            _http = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public T Get<T>(string url)
        {
            var result = _http.GetAsync(url).Result;
            var payload = Decompress(result.Content.ReadAsByteArrayAsync().Result);
            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(payload);
            }
            else
            {
                throw new Exception(payload);
            }
        }

        //From https://www.dotnetperls.com/decompress
        private string Decompress(byte[] gzip)
        {
            using (var stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                var size = 4096;
                var buffer = new byte[size];
                using (var memory = new MemoryStream())
                {
                    var count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    } while (count > 0);
                    return Encoding.Default.GetString(memory.ToArray());
                }
            }
        }
    }
}
