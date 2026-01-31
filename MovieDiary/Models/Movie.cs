using SQLite;
using System.Text.Json.Serialization;

namespace MovieDiary.Models
{
    // Třída reprezentující film
    public class Movie
    {
        [PrimaryKey, AutoIncrement] // Pro SQLite databázi
        public int Id { get; set; }

        [JsonPropertyName("Title")] // Mapování z JSONu (API)
        public string Title { get; set; }

        [JsonPropertyName("Year")]
        public string Year { get; set; }

        [JsonPropertyName("imdbID")]
        public string ImdbID { get; set; }

        [JsonPropertyName("Poster")]
        public string PosterUrl { get; set; }

        // Naše uživatelská data (nejsou v API, vyplníme my)
        public int MyRating { get; set; }     // Hodnocení 1-5
        public string MyNote { get; set; }    // Poznámka
        public bool IsWatched { get; set; } = false;
    }

    // Pomocná třída, protože OMDb vrací seznam filmů zabalený v objektu "Search"
    public class SearchResult
    {
        [JsonPropertyName("Search")]
        public List<Movie> Movies { get; set; }
    }
}