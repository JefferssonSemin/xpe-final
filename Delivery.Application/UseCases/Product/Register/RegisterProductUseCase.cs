using AutoMapper;
using Delivery.Comunication.RequestModel.Product;
using Delivery.Comunication.ResponseModel.Product;
using Delivery.Domain.Repositories;
using Delivery.Domain.Repositories.Product;
using Delivery.Exception;

namespace Delivery.Application.UseCases.Product.Register;

public class RegisterProductUseCase(IMapper mapper, IUnitOfWork uow, IProductWriteOnlyRepository repository) : IRegisterProductUseCase
{
    public async Task<ResponseProductJson> ExecuteAsync(RequestProductJson request)
    {
        await Validate(request);
        var product = mapper.Map<Domain.Entities.Product>(request);

        await repository.SaveAsync(product);

        await uow.CommitAsync();
            
        return mapper.Map<ResponseProductJson>(product);
    }
    
    private static async Task Validate(RequestProductJson requestRegister)
    {
        var validator = new RegisterProductValidator();

        var result = await validator.ValidateAsync(requestRegister);

        if (result.IsValid) return;

        var errorsMessage = result.Errors.Select(f => f.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorsMessage);
    }
}