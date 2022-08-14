using MauiApp1.Services;

namespace MauiApp1;

public partial class App : Application
{
	private readonly ITestService _testService;

	public App(ITestService testService,TestPage testPage)
	{
		this._testService = testService;

        InitializeComponent();

        MainPage = testPage;
    }
}
