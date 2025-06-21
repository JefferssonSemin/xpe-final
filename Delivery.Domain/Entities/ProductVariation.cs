namespace Delivery.Domain.Entities;

public class ProductVariation{
    
    public long Id { get; set; }
    public string Cod { get; set; }
    public bool Enable { get; set; }
    public string Name { get; set; } 
    public DateTime CreatedAt { get; set; }
    public Guid Guid { get; set; }
    
    public long ProductId { get; set; }
    public Product Product { get; set; } 
}   