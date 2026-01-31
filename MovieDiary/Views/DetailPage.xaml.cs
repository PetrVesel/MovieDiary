using MovieDiary.Models;
using MovieDiary.Services;

namespace MovieDiary.Views;

// QueryProperty nám umožní pøijmout data z navigace
[QueryProperty(nameof(MovieArg), "Movie")]
public partial class DetailPage : ContentPage
{
    MovieService _service;
    Movie _movie;

    public Movie MovieArg
    {
        set
        {
            _movie = value;
            LoadMovieData();
        }
    }

    public DetailPage(MovieService service)
    {
        InitializeComponent();
        _service = service;
    }

    void LoadMovieData()
    {
        // Naplnìní UI daty
        lblTitle.Text = _movie.Title;
        lblYear.Text = _movie.Year;
        imgPoster.Source = _movie.PosterUrl;

        // Pokud už je film v DB (má ID > 0), naèteme i poznámku
        if (_movie.Id != 0)
        {
            sliderRating.Value = _movie.MyRating > 0 ? _movie.MyRating : 3;
            editorNote.Text = _movie.MyNote;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Uložení dat do perzistentní pamìti
        _movie.MyRating = (int)sliderRating.Value;
        _movie.MyNote = editorNote.Text;
        _movie.IsWatched = true;

        await _service.AddOrUpdateMovieAsync(_movie);
        await DisplayAlert("Hotovo", "Film byl uložen do tvého deníku.", "OK");
        await Shell.Current.GoToAsync(".."); // Návrat zpìt
    }
}