using Windows.Media.Core;

namespace TubePlayer.Presentation;

public partial record VideoDetailsModel(YoutubeVideo Video)
{
	public IFeed<MediaSource> VideoSource => Feed.Async((ct) => new ValueTask<MediaSource>());
}
