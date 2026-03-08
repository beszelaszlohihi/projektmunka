using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Driver_contracts
    {
        [Key]
        public int contract_id { get; set; }
        public int driver_id { get; set; }
        public int team_id { get; set; }
        public int season_year{ get; set; }
        public decimal salary_estimate{ get; set; }
    }
}
