
using System.Text.Json;

namespace ViewModels
{
    public class mmoraSearchViewModel
    {
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
                var movieResponse = JsonSerializer.Deserialize<MovieResponse>(response);

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

        private async Task SaveMovieToDatabaseAsync(Movie movie)
        {
            // Guardar en SQLite
            var database = await MovieDatabase.Instance;
            await database.SaveMovieAsync(new MovieEntity
            {
                Title = movie.Title,
                Genre = movie.Genres.FirstOrDefault(),
                MainActor = movie.Actors.FirstOrDefault(),
                Awards = movie.Awards,
                Website = movie.Website,
                Mmora = "Mmora"
            });
        }
    }
}