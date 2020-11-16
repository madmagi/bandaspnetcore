using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace apicore.Models
{
    public class Band
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Year")]
        public int Year { get; set; }

        [JsonPropertyName("Rating")]
        public ushort Rating { get; set; }
    }
}
