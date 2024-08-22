namespace TubePlayer.Presentation;

public sealed partial class VideoDetailsPage : Page
{
	public VideoDetailsPage()
	{
		this.InitializeComponent();
	}

	protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
	{
		base.OnNavigatingFrom(e);

		youtubePlayer?.MediaPlayer.Pause();
	}
}

