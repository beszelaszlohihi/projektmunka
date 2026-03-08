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
        public int season_year { get; set; }
        public int circuit_id { get; set; }
        public DateTime race_date { get; set; }
        public int round_number { get; set; }
    }
}
