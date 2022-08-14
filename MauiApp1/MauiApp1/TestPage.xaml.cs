using MauiApp1.Services;
using Microsoft.Maui.Dispatching;

namespace MauiApp1;

public partial class TestPage : ContentPage
{
    private readonly ITestService _testService;

    public TestPage(ITestService testService)
    {
        InitializeComponent();
        this._testService = testService;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        string message = _testService.GetString();
        DisplayAlert("title", message, "È¡Ïû");
    }
}