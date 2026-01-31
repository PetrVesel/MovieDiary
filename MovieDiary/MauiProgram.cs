using Microsoft.Extensions.Logging;
using MovieDiary.Services;
using MovieDiary.Views;

namespace MovieDiary
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MovieService>();

            builder.Services.AddTransient<SearchPage>();
            builder.Services.AddTransient<LibraryPage>();
            builder.Services.AddTransient<DetailPage>();


            return builder.Build();
        }
    }
}
