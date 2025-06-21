using Delivery.Comunication.RequestModel.ElasticSearch;

namespace Delivery.Application.UseCases.Product.Search;

public interface ISearchProductUseCase
{
     Task<ProductDocument> ExecuteAsync(string query);
}