namespace TubePlayer.Presentation
{
	public sealed partial class VideoDetailsPage : Page
	{
		public VideoDetailsPage()
		{
			this.DataContext<BindableVideoDetailsModel>((page, vm) => page
				.Background(Theme.Brushes.Background.Default)
				.Content(new Grid()
					.SafeArea(SafeArea.InsetMask.All)
					.Children(
					new NavigationBar()
						.Content("Second Page")
						.MainCommand(new AppBarButton()
							.Icon(new BitmapIcon().UriSource(new Uri("ms-appx:///TubePlayer/Assets/Icons/back.png")))
						),
					new TextBlock()
						.HorizontalAlignment(HorizontalAlignment.Center)
						.VerticalAlignment(VerticalAlignment.Center))));
		}
	}

}
