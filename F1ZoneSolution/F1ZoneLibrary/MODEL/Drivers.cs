using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Drivers
    {
        [Key]
        public int driver_id { get; set; }
        public string driver_name { get; set; }
        public string nationality { get; set; }
        public DateTime birth_date { get; set; }
        public int debut_year { get; set; }
        public int championships { get; set; }
        public string biography { get; set; }
    }
}
