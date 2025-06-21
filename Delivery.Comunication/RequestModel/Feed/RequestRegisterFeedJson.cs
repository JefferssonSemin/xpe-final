namespace Delivery.Comunication.RequestModel.Feed;

public class RequestRegisterFeedJson(int duration, string videoUrl, string thumbnailUrl, string title)
{
    public string Title { get; } = title;
    public string ThumbnailUrl { get; } = thumbnailUrl;
    public string VideoUrl { get; } = videoUrl;
    public int Duration { get; } = duration;
}