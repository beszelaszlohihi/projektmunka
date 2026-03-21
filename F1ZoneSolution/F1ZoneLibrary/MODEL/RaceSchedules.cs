using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace F1ZoneLibrary.MODEL
{
    public class RaceSchedules
    {
        public int Id { get; set; }
        public string RaceName { get; set; }
        public string TrackName { get; set; }
        public DateTime RaceDate { get; set; }
        public string CountryCode { get; set; }
    }
}
