using System.Linq;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ExamenProgreso3_MauricioMora.Models;

namespace ExamenProgreso3_MauricioMora.ViewModels
{
    public class mmoraSearchViewModel : BaseViewModel
    {
        private string _movieDetails;

        public string MovieDetails
        {
            get => _movieDetails;
            set
            {
                _movieDetails = value;
                OnPropertyChanged(); // Notifica el cambio de propiedad a la vista
            }
        }

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
                    MovieDetails = $"Título: {movie.Title}\n" +
                                   $"Género: {string.Join(", ", movie.Genre)}\n" +
                                   $"Actor Principal: {movie.Actors.FirstOrDefault()}\n" +
                                   $"Awards: {movie.Awards}\n" +
                                   $"Website: {movie.Website}";

                    await SaveMovieToDatabaseAsync(movie);
                }
                else
                {
                    MovieDetails = "Película no encontrada.";
                }
            }
            catch (Exception ex)
            {
                MovieDetails = $"Error: {ex.Message}";
            }
        }

        private async Task SaveMovieToDatabaseAsync(Movie movie)
        {
            // Usamos GetInstanceAsync() en lugar de Instance
            var database = await MovieDatabase.GetInstanceAsync();
            await database.SaveMovieAsync(new MovieEntity
            {
                Title = movie.Title,
                Genre = movie.Genre.FirstOrDefault(),
                MainActor = movie.Actors.FirstOrDefault(),
                Awards = movie.Awards,
                Website = movie.Website,
                Mmora = "Mauricio Mora"
            });
        }
    }
}
