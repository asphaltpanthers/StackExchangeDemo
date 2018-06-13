using SED.Platform.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SED.Platform.Clients
{
    public class StackExchange
    {
        private Http _http;

        public StackExchange()
        {
            //Use the App.config to support testing multiple environments.
            _http = new Http(ConfigurationManager.AppSettings["URL"]);
        }

        public ItemsModel<Question> GetQuestions(IEnumerable<string> tags, string site, int page)
        {
            //Convert list of tags into a url encoded string.
            var tagged = HttpUtility.UrlEncode(string.Join(";", tags));

            return _http.Get<ItemsModel<Question>>("questions?tagged=" + tagged + "&site=" + site + "&page=" + page + "&pagesize=100&key=U4DMV*8nvpm3EOpvf69Rxw((");
        }

        public ItemsModel<User> GetUsers(IEnumerable<int> ids, string site)
        {
            //Convert list of ids into a url encoded string.
            var query = HttpUtility.UrlEncode(string.Join(";", ids));
            return _http.Get<ItemsModel<User>>("users/" + query + "?site=" + site + "&pagesize=100&key=U4DMV*8nvpm3EOpvf69Rxw((");
        }
    }
}
