using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.Dto
{
    public class RaceDto
    {
        //ez a raceschedules tablahoz kell, illetve a versenynaptar oldalhoz
        public int Id { get; set; }
        public string RaceName { get; set; }
        public string TrackName { get; set; }
        public DateTime RaceDate { get; set; }
        public string CountryCode { get; set; }
    }
}
