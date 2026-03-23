using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class HomepageSettings
    {
        [Key]
        public int Id { get; set; }
        public int FeaturedCircuitId { get; set; }
        public int FeaturedDriverId { get; set; }
    }
}
