namespace Delivery.Comunication.ResponseModel.Product;

public class ResponseProductJson(string cod, string unitMeasure, string name, string description)
{
    public string Cod { get; set; } = cod;
    public string Name { get; set; } = name;
}