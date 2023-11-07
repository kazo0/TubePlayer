using System.Text.Json;

namespace TubePlayer
{
	public class App : Application
	{
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
					.UseSerialization(services =>
					{
						services.AddSingleton(new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
					})
					.ConfigureServices((context, services) =>
					{
						services.AddSingleton<IYoutubeService, YoutubeServiceMock>();
					})
					.UseNavigation(ReactiveViewModelMappings.ViewModelMappings, RegisterRoutes)
				);
			MainWindow = builder.Window;

#if DEBUG
			MainWindow.EnableHotReload();
#endif

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
					Nested: new RouteMap[]
					{
						new RouteMap("Main", View: views.FindByViewModel<MainModel>()),
						new RouteMap("VideoDetails", View: views.FindByViewModel<VideoDetailsModel>()),
					}
				)
			);
		}
	}
}
