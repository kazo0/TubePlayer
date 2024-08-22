using Uno.Resizetizer;

namespace TubePlayer;
public partial class App : Application
{
	/// <summary>
	/// Initializes the singleton application object. This is the first line of authored code
	/// executed, and as such is the logical equivalent of main() or WinMain().
	/// </summary>
	public App()
	{
		this.InitializeComponent();
	}

	protected Window? MainWindow { get; private set; }
	protected IHost? Host { get; private set; }

	protected async override void OnLaunched(LaunchActivatedEventArgs args)
	{
		var builder = this.CreateBuilder(args)
			// Add navigation support for toolkit controls such as TabBar and NavigationView
			.UseToolkitNavigation()
			.Configure(host => host
#if DEBUG
				// Switch to Development environment when running in DEBUG
				.UseEnvironment(Environments.Development)
#endif
				.UseConfiguration(configure: configBuilder =>
					configBuilder
						.EmbeddedSource<App>()
						.Section<AppConfig>()
				)
				.ConfigureServices((context, services) =>
				{
					services.AddSingleton<IYoutubeService, YoutubeServiceMock>();
				})
				.UseSerialization(services =>
				{
					services.AddSingleton(new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
				})
				.UseNavigation(ReactiveViewModelMappings.ViewModelMappings, RegisterRoutes)
			);
		MainWindow = builder.Window;

#if DEBUG
        MainWindow.UseStudio();
#endif
		MainWindow.SetWindowIcon();

		Host = await builder.NavigateAsync<Shell>();
	}

	private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
	{
		views.Register(
			new ViewMap(ViewModel: typeof(ShellModel)),
			new ViewMap<MainPage, MainModel>(),
			new DataViewMap<VideoDetailsPage, VideoDetailsModel, Entity>()
		);

		routes.Register(
			new RouteMap("", View: views.FindByViewModel<ShellModel>(),
				Nested:
				[
					new ("Main", View: views.FindByViewModel<MainModel>(), IsDefault:true),
					new ("VideoDetails", View: views.FindByViewModel<VideoDetailsModel>()),
				]
			)
		);
	}
}
