using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExamenProgreso3_MauricioMora.Models
{
    public class MovieResponse
    {
        [JsonPropertyName("Movies")]
        public List<Movie> Movies { get; set; }
    }

    public class Movie
    {
        public string Title { get; set; }
        public List<string> Genre { get; set; }
        public List<string> Actors { get; set; }
        public string Awards { get; set; }
        public string Website { get; set; }
    }

}
