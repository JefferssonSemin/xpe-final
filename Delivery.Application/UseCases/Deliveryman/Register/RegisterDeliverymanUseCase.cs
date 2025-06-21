using AutoMapper;
using Delivery.Comunication.RequestModel.Deliveryman;
using Delivery.Comunication.ResponseModel.Deliveryman;
using Delivery.Domain.Repositories;
using Delivery.Domain.Repositories.Deliveryman;
using Delivery.Domain.Security.Logged;
using Delivery.Exception;

namespace Delivery.Application.UseCases.Deliveryman.Register;

public class RegisterDeliverymanUseCase(
    IDeliverymanWriteOnlyRepository deliverymanWriteOnlyRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRegisterDeliverymanUseCase
{
    public async Task<ResponseRegisterDeliverymanJson> ExecuteAsync(RequestRegisterDeliverymanJson requestRegister)
    {
        Validate(requestRegister);

        var user = await loggedUser.Get();

        var deliveryman = mapper.Map<Domain.Entities.Deliveryman>(requestRegister);
        
        deliveryman.AddUser(user);

        await deliverymanWriteOnlyRepository.AddAsync(deliveryman);

        await unitOfWork.CommitAsync();

        return mapper.Map<ResponseRegisterDeliverymanJson>(deliveryman);
    }

    private static void Validate(RequestRegisterDeliverymanJson requestRegister)
    {
        var validator = new RegisterDeliveryValidator();

        var result = validator.Validate(requestRegister);

        if (result.IsValid) return;

        var errorsMessage = result.Errors.Select(f => f.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorsMessage);
    }
}