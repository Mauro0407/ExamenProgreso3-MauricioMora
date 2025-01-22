using ExamenProgreso3_MauricioMora.ViewModels;

namespace ExamenProgreso3_MauricioMora.Views;

public partial class mmoraSearchPage : ContentPage
{
    public mmoraSearchPage()
    {
        InitializeComponent();
        BindingContext = new mmoraSearchViewModel();  // Vincula el ViewModel aquí
    }
}
