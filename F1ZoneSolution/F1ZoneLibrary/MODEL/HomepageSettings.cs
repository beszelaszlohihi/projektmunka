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
        public int FeaturedCircuitId { get; set; } //fő oldalon a pálya id-jét tárolja
        public int FeaturedDriverId { get; set; }  //fő oldalon a pilóta id-jét tárolja
    }
}
