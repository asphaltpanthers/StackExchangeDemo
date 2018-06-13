using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SED.Platform.Entities
{
    public class Question
    {
        public IEnumerable<string> Tags { get; set; }
        public Owner Owner { get; set; }
        public bool Is_Answered { get; set; }
        public int View_Count { get; set; }
        public int Answer_Count { get; set; }
        public int Score { get; set; }
        public int Last_Activity_Date { get; set; }
        public int Creation_Date { get; set; }
        public int Last_Edit_Date { get; set; }
        public int Question_Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public int Weight { get; set; }
    }
}
