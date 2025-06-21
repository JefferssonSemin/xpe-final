using Delivery.Comunication.RequestModel.Product;
using Delivery.Comunication.ResponseModel.Product;

namespace Delivery.Application.UseCases.Product.Register;

public interface IRegisterProductUseCase
{
    Task<ResponseProductJson> ExecuteAsync(RequestProductJson request);
}