namespace ExamenProgreso3_MauricioMora.Views;

public partial class mmoraConsultedPage : ContentPage
{
    public mmoraConsultedPage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.mmoraConsultedViewModel();
    }
}