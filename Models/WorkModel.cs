using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktopapp1.Models
{
    public class WorkModel
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string worklog { get; set; }
        public string key { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string logged { get; set; }
        public string @break { get; set; }
        public bool conflict { get; set; }
    }
}
