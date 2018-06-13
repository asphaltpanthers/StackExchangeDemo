using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SED.Platform.Entities
{
    public class ItemsModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public bool Has_More { get; set; }
    }
}
