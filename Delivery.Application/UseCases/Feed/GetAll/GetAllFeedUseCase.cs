using AutoMapper;
using Delivery.Comunication.ResponseModel.Feed;
using Delivery.Domain.Repositories.Feed;

namespace Delivery.Application.UseCases.Feed.GetAll;

public class GetAllFeedUseCase(IMapper mapper, IFeedReadOnlyRepository repository) : IGetAllFeedUseCase
{
    public async Task<List<ResponseFeedJson>> ExecuteAsync()
    {
        var feeds = await  repository.GetAllAsync();
        
        return mapper.Map<List<ResponseFeedJson>>(feeds);
    }
}