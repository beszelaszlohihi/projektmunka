using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.Dto
{
    public class DriverDto
    {
        public int driver_id { get; set; }
        public string driver_name { get; set; } = "";
        public string nationality { get; set; } = "";
        public int debut_year { get; set; }
        public string biography { get; set; } = "";

        
        public int championships { get; set; }
        public int wins { get; set; }
        public int podiums { get; set; }
        public int fastest_laps { get; set; }
        public decimal points { get; set; }
    }
}
