using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Circuits
    {
        [Key]
        public int circuit_id { get; set; }
        public string circuit_name { get; set; }
        public string location { get; set; }
        public string country { get; set; }
        public decimal length_km { get; set; }
        public int turns { get; set; }
    }
}
