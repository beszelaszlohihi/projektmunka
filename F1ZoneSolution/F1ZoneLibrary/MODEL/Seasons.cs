using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class Seasons
    {
        [Key]
        public int season_year { get; set; }
        public string technical_regulations { get; set; }
    }
}
