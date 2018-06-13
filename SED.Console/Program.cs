using SED.Platform;
using SED.Platform.Clients;
using SED.Platform.Entities;
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
            //Initilize the StackExchange client.
            var stackExchange = new StackExchange();

            //Get the questions.
            var questions = GetQuestions(stackExchange);

            //Get user ids.
            var users = questions.Select(q => q.Owner.User_Id).Distinct();

            //Get user weights
            foreach (var user in users)
            {
                var weight = GetWeight(stackExchange.GetUser(user, "stackoverflow").Items.First().Badge_Counts);
            }
        }

        private static IEnumerable<Question> GetQuestions(StackExchange stackExchange)
        {
            //The API will return a maximum of 100 questions, so we need to request multiple
            //pages of questions until we have them all.
            var hasMore = true;
            var page = 1;
            var questions = new List<Question>();
            while (hasMore)
            {
                //The instructions state to get questions with C#, .NET, and Selenium tags.
                //If we wanted to get instructions with C#, .NET, or Selenium tags, I would
                //have to make multiple requests. If we wanted to get instructions with only
                //C#, .NET, and Selenium tags, I would have to filter these questions some
                //other way.
                var model = stackExchange.GetQuestions(new List<string>()
                {
                    "C#",
                    ".NET",
                    "Selenium"
                }, "stackoverflow", page);

                questions.AddRange(model.Items);
                hasMore = model.Has_More;
                page++;
            }

            return questions;
        }

        private static int GetWeight(BadgeCounts badgeCounts)
        {
            //The instruction don't give explicit badge weights so I'll make up my own.
            //Gold = 3
            //Silver = 2
            //Bronze = 1
            return (badgeCounts.Gold * 3) + (badgeCounts.Silver * 2) + badgeCounts.Bronze;
        }
    }
}
