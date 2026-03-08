using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Pit_stops
    {
        [Key]
        public int stop_id { get; set; }
        public int race_id { get; set; }
        public int driver_id { get; set; }
        public int stop_number { get; set; }
        public int lap { get; set; }
        public decimal duration_seconds { get; set; }
    }
}
