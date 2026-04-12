using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Team_sponsors
    {
        [Key]
        public int team_id { get; set; }
        public int sponsor_id { get; set; }
        public int contract_value{ get; set; }
    }
}
