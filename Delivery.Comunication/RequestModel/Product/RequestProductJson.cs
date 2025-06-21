namespace Delivery.Comunication.RequestModel.Product;

public class RequestProductJson(string cod, string unitMeasure, string name, string description)
{
    public string Cod { get; set; } = cod;
    public string UnitMeasure { get; set; } = unitMeasure;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
}