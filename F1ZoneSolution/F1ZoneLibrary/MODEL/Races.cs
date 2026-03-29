using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Races
    {
        [Key]
        public int race_id { get; set; }
        //public int season_year { get; set; }
        public int winner_driver_id { get; set; }
        public TimeSpan fastest_lap_time { get; set; }
        public int laps_completed { get; set; }
        public int fastest_lap_driver_id{ get; set; }
        public int race_time{ get; set; }
    }
}
