using System.Collections.ObjectModel;

namespace ViewModels
{
    public class mmoraConsultedViewModel : BaseViewModel
    {
        public ObservableCollection<MovieEntity> Movies { get; set; }
        public object MovieDatabase { get; private set; }

        public mmoraConsultedViewModel()
        {
            LoadMoviesAsync();
        }

        private async Task LoadMoviesAsync()
        {
            var database = await MovieDatabase.Instance;
            Movies = new ObservableCollection<MovieEntity>(await database.GetMoviesAsync());
        }
    }
}