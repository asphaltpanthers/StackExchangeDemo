using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            var payload = result.Content.ReadAsStringAsync().Result;
            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(payload);
            }
            else
            {
                throw new Exception(payload);
            }
        }
    }
}
