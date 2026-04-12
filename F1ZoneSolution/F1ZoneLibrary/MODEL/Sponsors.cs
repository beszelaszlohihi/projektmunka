using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Sponsors
    {
        [Key]
        public int sponsor_id { get; set; }
        public string sponsor_name { get; set; }
        public string industry { get; set; }
    }
}
