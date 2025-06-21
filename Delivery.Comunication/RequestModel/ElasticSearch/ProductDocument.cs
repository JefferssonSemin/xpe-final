using Nest;

namespace Delivery.Comunication.RequestModel.ElasticSearch;

[ElasticsearchType(IdProperty = nameof(ProductId))] 
public class ProductDocument
{
    public string ProductId { get; set; }

    [Text(Name = "name", Analyzer = "portuguese_analyzer")]
    public string Name { get; set; }

    [Text(Name = "description", Analyzer = "portuguese_analyzer")]
    public string Description { get; set; }
     
    [Completion(Name = "suggest")]
    public CompletionField Suggest { get; set; }

    public ProductDocument()
    {
        Suggest = new CompletionField(); 
    }
}