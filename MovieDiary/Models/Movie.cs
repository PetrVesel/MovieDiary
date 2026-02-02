using SQLite;
using System.Text.Json.Serialization;

namespace MovieDiary.Models
{
    //Třída reprezentující film
    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("Year")]
        public string Year { get; set; }

        [JsonPropertyName("imdbID")]
        public string ImdbID { get; set; }

        [JsonPropertyName("Poster")]
        public string PosterUrl { get; set; }

        //Uživatelská data
        public int MyRating { get; set; } 
        public string MyNote { get; set; }
        public bool IsWatched { get; set; } = false;
    }

    public class SearchResult
    {
        [JsonPropertyName("Search")]
        public List<Movie> Movies { get; set; }
    }
}