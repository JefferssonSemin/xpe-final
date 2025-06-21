namespace Delivery.Domain.Entities;

public class Feed
{
    public long Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string ThumbnailUrl { get; init; } = string.Empty;
    public string VideoUrl { get; init; } = string.Empty;
    public int Likes { get; set; }
    public int Duration { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    public long? UserId { get; set; }
    public User User { get; init; } = null!;
    
    public void AddUser(User user)
    {
        UserId = user.Id;
    }
    
    public void UpdateLike()
    {
        Likes++;
    }
}