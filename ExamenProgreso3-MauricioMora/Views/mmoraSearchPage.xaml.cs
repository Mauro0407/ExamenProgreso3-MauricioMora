using ViewModels;

namespace ExamenProgreso3_MauricioMora.Views;

public partial class mmoraSearchPage : ContentPage
{
    private readonly ViewModels.mmoraSearchViewModel _viewModel;

    public mmoraSearchPage()
    {
        InitializeComponent();
        _viewModel = new mmoraSearchViewModel();
        BindingContext = _viewModel;
    }

    private async void OnSearchButtonClicked(object sender, EventArgs e)
    {
        await _viewModel.SearchMovieAsync(mmora_entrySearch.Text);
    }

    private void OnClearButtonClicked(object sender, EventArgs e)
    {
        mmora_entrySearch.Text = string.Empty;
        mmora_lblResult.Text = "Resultado de búsqueda...";
    }
}
}