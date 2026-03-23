using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace F1ZoneLibrary.Dto
{
    public class DriverDto
    {
        [JsonPropertyName("driver_id")]
        public int driver_id { get; set; }
        [JsonPropertyName("driver_name")]
        public string driver_name { get; set; } = "";
        public string? nationality { get; set; } = "";
        public int? debut_year { get; set; }
        public string? biography { get; set; } = "";
        public string? teamname { get; set; }
        [JsonPropertyName("championships")]
        public int? championships { get; set; }
        [JsonPropertyName("wins")]
        public int? wins { get; set; }
        public int? podiums { get; set; }
        public int? fastest_laps { get; set; }
        [JsonPropertyName("points")]
        public decimal? points { get; set; }
        public string? teamcolor { get; set; }
        public int? racing_number { get; set; }
    }
}
