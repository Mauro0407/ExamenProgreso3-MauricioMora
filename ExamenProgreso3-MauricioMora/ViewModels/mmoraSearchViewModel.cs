using System.Linq;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ExamenProgreso3_MauricioMora.Models;
using System.Windows.Input;

namespace ExamenProgreso3_MauricioMora.ViewModels
{
    public class mmoraSearchViewModel : BaseViewModel
    {
        private string _movieDetails;
        private string _searchQuery;

        public string MovieDetails
        {
            get => _movieDetails;
            set
            {
                _movieDetails = value;
                OnPropertyChanged(); // Notifica el cambio de propiedad a la vista
            }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand ClearCommand { get; }

        public mmoraSearchViewModel()
        {
            // Comandos
            SearchCommand = new Command(async () => await SearchMovieAsync(SearchQuery));
            ClearCommand = new Command(ClearSearch);
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

                // Muestra la respuesta JSON para depuración
                Console.WriteLine(response);

                var movieResponse = JsonSerializer.Deserialize<MovieResponse>(response);

                if (movieResponse != null && movieResponse.Movies.Any())
                {
                    var movie = movieResponse.Movies.First();
                    MovieDetails = $"Título: {movie.Title}\n" +
                                   $"Género: {string.Join(", ", movie.Genre)}\n" +
                                   $"Actor Principal: {movie.Actors.FirstOrDefault()}\n" +
                                   $"Awards: {movie.Awards}\n" +
                                   $"Website: {movie.Website}";
                }
                else
                {
                    MovieDetails = "Película no encontrada.";
                }
            }
            catch (JsonException jsonEx)
            {
                // Captura y muestra detalles del error de deserialización
                MovieDetails = $"Error al procesar el JSON: {jsonEx.Message}";
            }
            catch (Exception ex)
            {
                MovieDetails = $"Error: {ex.Message}";
            }
        }

        // Método para limpiar la búsqueda
        private void ClearSearch()
        {
            SearchQuery = string.Empty;  // Limpia la consulta de búsqueda
            MovieDetails = string.Empty; // Limpia los detalles de la película
        }
    }
}
