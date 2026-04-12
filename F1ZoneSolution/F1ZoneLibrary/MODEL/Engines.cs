using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Engines
    {
        [Key]
        public int engine_id { get; set; }
        public string engine_name { get; set; }
        public string manufacturer { get; set; }
    }
}
