namespace Delivery.Domain.Entities;

public class Deliveryman(DateOnly? dateOfBirth, string name, string cpf, string email)
{
    public int Id { get; init; }
    public string Name { get; init; } = name;
    public string Email { get; init; } = email;
    public string Cpf { get; init; } = cpf;
    public DateOnly? DateOfBirth { get; init; } = dateOfBirth;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    public long? UserId { get; set; }
    public User User { get; init; } = null!;
        
        
    public void AddUser(User user)
    {
        UserId = user.Id;
    }
}