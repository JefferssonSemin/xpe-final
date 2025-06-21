using AutoMapper;
using Delivery.Comunication.ResponseModel.Feed;
using Delivery.Domain.Repositories.Feed;

namespace Delivery.Application.UseCases.Feed.GetById;

public class GetByIdFeedUseCase(IFeedReadOnlyRepository readRepository, IMapper mapper) : IGetByIdFeedUseCase
{
    public async Task<ResponseFeedJson> ExecuteAsync(int id)
    {
        var feed = await readRepository.GetByIdAsync(id);
        
        return mapper.Map<ResponseFeedJson>(feed);
    }
}