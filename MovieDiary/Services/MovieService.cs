using MovieDiary.Models;
using SQLite;
using System.Net.Http.Json;

namespace MovieDiary.Services
{
    public class MovieService
    {
        // 1. Část: Databáze
        SQLiteAsyncConnection _database;

        async Task Init()
        {
            if (_database is not null) return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "MyMovies.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            await _database.CreateTableAsync<Movie>();
        }

        public async Task AddOrUpdateMovieAsync(Movie movie)
        {
            await Init();
            await _database.InsertOrReplaceAsync(movie); // Uloží nebo přepíše
        }

        public async Task<List<Movie>> GetMyMoviesAsync()
        {
            await Init();
            return await _database.Table<Movie>().ToListAsync();
        }

        public async Task DeleteMovieAsync(Movie movie)
        {
            await Init();
            await _database.DeleteAsync(movie);
        }

        // 2. Část: REST API
        HttpClient _client;
        // !!! ZDE VLOŽTE VÁŠ API KLÍČ Z EMAILU !!!
        const string ApiKey = "8073d6a2";
        const string Url = "https://www.omdbapi.com/";

        public MovieService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Movie>> SearchMoviesAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return new List<Movie>();

            // Sestavení URL
            var response = await _client.GetAsync($"{Url}?apikey={ApiKey}&s={query}&type=movie");

            if (response.IsSuccessStatusCode)
            {
                // Deserializace JSONu
                var result = await response.Content.ReadFromJsonAsync<SearchResult>();
                return result?.Movies ?? new List<Movie>();
            }

            return new List<Movie>();
        }
    }
}