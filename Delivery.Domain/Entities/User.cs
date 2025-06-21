using Delivery.Domain.Enums;

namespace Delivery.Domain.Entities;

public class User(string password)
{
    public long Id { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string Password { get; set; } = password;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public Guid Guid { get; init; } = Guid.NewGuid();
    public string Role { get; init; } = Roles.Admin;
    
    public void AddPassword(string password)
    {
        Password = password;
    } 
}