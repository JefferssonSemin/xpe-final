namespace Delivery.Comunication.ResponseModel.Feed;

public class ResponseFeedJson
{
    public long Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string VideoUrl { get; init; } = string.Empty;
    public int Duration { get; init; }
    public string ThumbnailUrl { get; init; } = string.Empty;
    public int Likes { get; init; }
}