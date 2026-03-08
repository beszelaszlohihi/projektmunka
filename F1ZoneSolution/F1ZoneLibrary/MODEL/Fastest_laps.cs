using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Fastest_laps
    {
        [Key]
        public int lap_id { get; set; }
        public int race_id { get; set; }
        public int driver_id { get; set; }
        public string lap_time { get; set; }
        public decimal average_speed { get; set; }
    }
}
