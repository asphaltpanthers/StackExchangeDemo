using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SED.Platform.Entities
{
    public class Owner
    {
        public int Reputation { get; set; }
        public int User_Id { get; set; }
        public string User_Type { get; set; }
        public int Accept_Rate { get; set; }
        public string Profile_Image { get; set; }
        public string Display_Name { get; set; }
        public string Link { get; set; }
    }
}
