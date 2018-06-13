using SED.Platform;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SED.Console
{
    //This task didn't seem like much of a test to me, so I decided to write it as a console app.
    //As an automated tester, I typically develop tests using a unit test framework such as NUnit.
    class Program
    {
        static void Main(string[] args)
        {
            //Initilize the HTTP client. If I were writing this code as an enterprise application,
            //I would implement a DI solution such as Unity. I don't think you're all that
            //interested in seeing that though.
            //Store the URL in the App.config to support testing multiple environments.
            var http = new Http(ConfigurationManager.AppSettings["URL"]);
        }
    }
}
