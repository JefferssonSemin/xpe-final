using AutoMapper;
using Delivery.Comunication.ResponseModel.Deliveryman;
using Delivery.Domain.Repositories.Deliveryman;
using Delivery.Exception;
using Delivery.Exception.ExceptionsBase;

namespace Delivery.Application.UseCases.Deliveryman.GetById;

public class GetByIdDeliverymanUseCase(IDeliverymanReadOnlyRepository repository, IMapper mapper)
    : IGetByIdDeliverymanUseCase
{  public async Task<ResponseDeliverymanJson> ExecuteAsync(int id)
     {
         var deliveryman = await repository.GetByIdAsync(id);
 
         if (deliveryman is null)
             throw new NotFoundException(ResourceErrorMessages.DELIVERYMAN_NOT_FOUND);
 
         return mapper.Map<ResponseDeliverymanJson>(deliveryman);
     }
 }
  