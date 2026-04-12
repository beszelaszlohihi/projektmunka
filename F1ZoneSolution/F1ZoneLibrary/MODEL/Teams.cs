using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Teams
    {
        [Key]
        public int team_id { get; set; }
        public string team_name { get; set; }
        public string country { get; set; }
        public int founded_year { get; set; }
        public int championships { get; set; }
        public int engine_id { get; set; }
    }
}
