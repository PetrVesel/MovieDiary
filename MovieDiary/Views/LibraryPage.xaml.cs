using MovieDiary.Models;
using MovieDiary.Services;

namespace MovieDiary.Views;

public partial class LibraryPage : ContentPage
{
    MovieService _service;

    public LibraryPage(MovieService service)
    {
        InitializeComponent();
        _service = service;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var myMovies = await _service.GetMyMoviesAsync();
        myList.ItemsSource = myMovies;
    }

    private async void OnMovieSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Movie selectedMovie)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Movie", selectedMovie }
            };
            await Shell.Current.GoToAsync(nameof(DetailPage), navigationParameter);
            myList.SelectedItem = null;
        }
    }
}