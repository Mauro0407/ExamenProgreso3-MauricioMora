using ExamenProgreso3_MauricioMora.Models;
using System.Collections.ObjectModel;
using ViewModels;

namespace ExamenProgreso3_MauricioMora.ViewModels
{
    public class mmoraConsultedViewModel : BaseViewModel
    {
        public ObservableCollection<MovieEntity> Movies { get; set; }

        public mmoraConsultedViewModel()
        {
            LoadMoviesAsync();
        }

        private async Task LoadMoviesAsync()
        {
            var database = await MovieDatabase.GetInstanceAsync();
            Movies = new ObservableCollection<MovieEntity>(await database.GetMoviesAsync());
        }
    }
}
