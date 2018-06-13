using Newtonsoft.Json;
using SED.Platform;
using SED.Platform.Clients;
using SED.Platform.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

            //Get the users.
            var users = GetUsers(stackExchange, questions);

            foreach (var question in questions)
            {
                question.Weight = GetWeight(users.First(u => u.User_Id == question.Owner.User_Id).Badge_Counts);
            }

            //I'm only going to write the top 10 questions.
            var topTenQuestions = questions.OrderByDescending(q => q.Weight).Take(10);

            //Write the top 10 questions to bin/Debug/TopTenQuestions.json.
            File.WriteAllText("TopTenQuestions.json", JsonConvert.SerializeObject(topTenQuestions));
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

            //Filter questions from users that don't exist.
            return questions.Where(q => q.Owner.User_Type != "does_not_exist");
        }

        private static IEnumerable<User> GetUsers(StackExchange stackExchange, IEnumerable<Question> questions)
        {
            //Get all the user ids associated to the questions.
            var userIds = questions.Select(q => q.Owner.User_Id).Distinct().ToList();

            //The maximum number of userIds I can query for is 100, so split the list up into
            //lists of size 100.
            //From https://stackoverflow.com/questions/11463734/split-a-list-into-smaller-lists-of-n-size?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
            var userLists = new List<List<int>>();
            for (var i = 0; i < questions.Count(); i += 100)
            {
                userLists.Add(userIds.GetRange(i, Math.Min(100, userIds.Count() - i)));
            }

            var users = new List<User>();
            foreach (var list in userLists)
            {
                users.AddRange(stackExchange.GetUsers(list, "stackoverflow").Items);
            }

            return users;
        }

        private static int GetWeight(BadgeCounts badgeCounts)
        {
            //The instructions don't give explicit badge weights so I'll make up my own.
            //Gold = 3
            //Silver = 2
            //Bronze = 1
            return (badgeCounts.Gold * 3) + (badgeCounts.Silver * 2) + badgeCounts.Bronze;
        }
    }
}
