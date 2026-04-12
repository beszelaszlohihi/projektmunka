using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Drivers
    {
        [Key]
        public int driver_id { get; set; }
        public string driver_name { get; set; } = string.Empty;
        public string? nationality { get; set; }
        public int? debut_year { get; set; }
        public int? championships { get; set; }
        public string? biography { get; set; }
        public int? wins { get; set; }
        public int? podiums { get; set; }
        public int? fastest_laps{ get; set; }
        public decimal? points { get; set; }
        public string? teamname{ get; set; }
        public string? teamcolor { get; set; }
        public int? racing_number { get; set; }
    }
}
