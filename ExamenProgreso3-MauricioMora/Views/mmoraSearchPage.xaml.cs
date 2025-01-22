using ExamenProgreso3_MauricioMora.ViewModels;

namespace ExamenProgreso3_MauricioMora.Views;

public partial class mmoraSearchPage : ContentPage
{
    private readonly mmoraSearchViewModel _viewModel;

    public mmoraSearchPage()
    {
        InitializeComponent();
        _viewModel = new mmoraSearchViewModel();
        BindingContext = _viewModel;
    }
}
