using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkeyEarthquakeSmsSenderSln.Models
{
    public class Earthquake
    {
        public string rms { get; set; }
        public string eventID { get; set; }
        public string location { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string depth { get; set; }
        public string type { get; set; }
        public string magnitude { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string neighborhood { get; set; }
        public string date { get; set; }
        public bool isEventUpdate { get; set; }
        public object lastUpdateDate { get; set; }
    }
}
