namespace Delivery.Domain.Entities;

public class Product
{
    public long Id { get; set; }
    public string Cod { get; set; } 
    public string UnitMeasure { get; set; } 
    public string Name { get; set; } 
    public string Description { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid Guid { get; set; } = Guid.NewGuid();
    
}