using AutoMapper;
using Delivery.Comunication.RequestModel.Deliveryman;
using Delivery.Comunication.RequestModel.Feed;
using Delivery.Comunication.RequestModel.Product;
using Delivery.Comunication.RequestModel.User;
using Delivery.Comunication.ResponseModel.Deliveryman;
using Delivery.Comunication.ResponseModel.Feed;
using Delivery.Comunication.ResponseModel.Product;
using Delivery.Comunication.ResponseModel.User;
using Delivery.Domain.Entities;

namespace Delivery.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterDeliverymanJson, Deliveryman>();
        CreateMap<RequestUpdateDeliverymanJson, Deliveryman>();
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());
        CreateMap<RequestUpdateUserJson, User>();
        CreateMap<RequestRegisterFeedJson, Feed>();
        CreateMap<RequestProductJson, Product>();
    }

    private void EntityToResponse()
    {
        CreateMap<Deliveryman, ResponseRegisterDeliverymanJson>();
        CreateMap<Deliveryman, ResponseDeliverymanJson>();
        CreateMap<User, ResponseUserJson>();
        CreateMap<User, ResponseUserProfileJson>();
        CreateMap<Feed, ResponseFeedJson>();
        CreateMap<Product, ResponseProductJson>();
    }
}