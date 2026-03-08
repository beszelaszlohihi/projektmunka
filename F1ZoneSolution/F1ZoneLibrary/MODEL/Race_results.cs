using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Race_results
    {
        [Key]
        public int result_id { get; set; }
        public int race_id { get; set; }
        public int driver_id { get; set; }
        public int team_id { get; set; }
        public int grid_position{ get; set; }
        public int final_position{ get; set; }
        public decimal points_earned{ get; set; }
        public string status{ get; set; }
    }
}
