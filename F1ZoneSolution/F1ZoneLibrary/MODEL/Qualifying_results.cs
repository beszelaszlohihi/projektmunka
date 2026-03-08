using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Qualifying_results
    {
        [Key]
        public int qualifying_id { get; set; }
        public int race_id { get; set; }
        public int driver_id { get; set; }
        public string q1_time { get; set; }
        public string q2_time { get; set; }
        public string q3_time { get; set; }
    }
}
