using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_HW.Models
{
    public class SearchTypeLog
    {
        public string type { get; set; }
        public string time { get; set; }

        public SearchTypeLog(string type, string time)
        {
            this.type = type;
            this.time = time;
        }
    }
}
