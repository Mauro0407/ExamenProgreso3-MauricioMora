
using System.Text.Json;

namespace ViewModels
{
    public class mmoraSearchViewModel
    {
        public object MovieDatabase { get; private set; }

        public async Task SearchMovieAsync(string movieName)
        {
            if (string.IsNullOrWhiteSpace(movieName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese un nombre", "OK");
                return;
            }

            try
            {
                string apiUrl = $"https://freetestapi.com/api/v1/movies?search={movieName}&limit=1";
                using var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(apiUrl);
                var movieResponse = JsonSerializer.Deserialize<mmoraMovieResponseViewModel>(response);

                if (movieResponse != null && movieResponse.Movies.Any())
                {
                    var movie = movieResponse.Movies.First();
                    await SaveMovieToDatabaseAsync(movie);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Película no encontrada", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task SaveMovieToDatabaseAsync(mmoraMovieViewModel movie)
        {
            // Guardar en SQLite
            var database = await MovieDatabase.Instance;
            await database.SaveMovieAsync(new mmoraMovieEntityViewModel
            {
                Titulo = movie.Titulo,
                Genero = movie.Generos.FirstOrDefault(),
                ActorPrincipal = movie.Actor.FirstOrDefault(),
                Premios = movie.Premios,
                SitioWeb = movie.SitioWeb,
                Mmora = "Mmora"
            });
        }
    }
}