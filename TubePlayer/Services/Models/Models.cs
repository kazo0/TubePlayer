namespace TubePlayer.Services.Models;

public record ThumbnailData(string? Url);

public record ThumbnailsData(ThumbnailData? Medium, ThumbnailData? High);

public record SnippetData(string? ChannelId, string? Title, string? Description, ThumbnailsData? Thumbnails, string? ChannelTitle, DateTime? PublishedAt);

public record StatisticsData(string? ViewCount, string? LikeCount, string? CommentCount, string? SubscriberCount);

public partial record ChannelData(
	string? Id,
	SnippetData? Snippet,
	StatisticsData? Statistics = default);

public record ContentDetailsData(string? Duration);

public partial record YoutubeVideoDetailsData(
	string? Id,
	SnippetData? Snippet,
	StatisticsData? Statistics = default,
	ContentDetailsData? ContentDetails = default);

public record VideoDetailsResultData(ImmutableList<YoutubeVideoDetailsData>? Items);

public record ChannelSearchResultData(ImmutableList<ChannelData>? Items);
