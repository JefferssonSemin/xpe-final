using AutoMapper;
using Delivery.Comunication.ResponseModel.Deliveryman;
using Delivery.Domain.Repositories.Deliveryman;

namespace Delivery.Application.UseCases.Deliveryman.GetAll;

public class GetAllDeliverymanUseCase(IDeliverymanReadOnlyRepository repository, IMapper mapper)
    : IGetAllDeliverymanUseCase
{
    public async Task<ResponseDeliverymenJson> ExecuteAsync()
    {
        var deliverymen = await repository.GetAllAsync();

        return new ResponseDeliverymenJson
            { Deliverymen = mapper.Map<List<ResponseRegisterDeliverymanJson>>(deliverymen) };
    }
}