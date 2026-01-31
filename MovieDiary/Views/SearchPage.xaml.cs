using MovieDiary.Models;
using MovieDiary.Services;

namespace MovieDiary.Views;

public partial class SearchPage : ContentPage
{
    MovieService _service;

    // Konstruktor dostane službu automaticky
    public SearchPage(MovieService service)
    {
        InitializeComponent();
        _service = service;
    }

    private async void OnSearchPressed(object sender, EventArgs e)
    {
        // Komunikace s REST API
        var term = searchBar.Text;
        var movies = await _service.SearchMoviesAsync(term);
        moviesList.ItemsSource = movies;
    }

    private async void OnMovieSelected(object sender, SelectionChangedEventArgs e)
    {
        // Navigace na detail
        if (e.CurrentSelection.FirstOrDefault() is Movie selectedMovie)
        {
            // Pøedáme vybraný film jako parametr navigace
            var navigationParameter = new Dictionary<string, object>
            {
                { "Movie", selectedMovie }
            };
            await Shell.Current.GoToAsync(nameof(DetailPage), navigationParameter);

            // Zrušit výbìr (aby šlo kliknout znovu)
            moviesList.SelectedItem = null;
        }
    }
}