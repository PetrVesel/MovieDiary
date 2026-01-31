using MovieDiary.Views;

namespace MovieDiary;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // Registrace routy pro navigaci
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
    }
}