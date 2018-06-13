using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SED.Platform.Entities
{
    public class User
    {
        public BadgeCounts Badge_Counts { get; set; }
        public int Account_Id { get; set; }
        public bool Is_Employee { get; set; }
        public int Last_Modified_date { get; set; }
        public int Last_Access_Date { get; set; }
        public int Reputation_Change_Year { get; set; }
        public int Reputation_Change_Quarter { get; set; }
        public int Reputation_Change_Month { get; set; }
        public int Reputation_Change_Week { get; set; }
        public int Reputation_CHange_Day { get; set; }
        public int Reputation { get; set; }
        public int Creation_Date { get; set; }
        public string User_Type { get; set; }
        public int User_Id { get; set; }
        public int Accept_Rate { get; set; }
        public string Location { get; set; }
        public string Website_Url { get; set; }
        public string Link { get; set; }
        public string Profile_Image { get; set; }
        public string Display_Name { get; set; }
    }
}
