using MauiApp1.Services;
using Microsoft.Maui.Dispatching;

namespace MauiApp1;

public partial class TestPage : ContentPage
{
    public TestPage()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("title", "message", "È¡Ïû");
    }
}