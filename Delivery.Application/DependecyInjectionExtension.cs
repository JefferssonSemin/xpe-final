using Delivery.Application.AutoMapper;
using Delivery.Application.Services;
using Delivery.Application.UseCases.Deliveryman.Delete;
using Delivery.Application.UseCases.Deliveryman.GetAll;
using Delivery.Application.UseCases.Deliveryman.GetById;
using Delivery.Application.UseCases.Deliveryman.Register;
using Delivery.Application.UseCases.Deliveryman.Reports.Excel;
using Delivery.Application.UseCases.Deliveryman.Reports.Pdf;
using Delivery.Application.UseCases.Deliveryman.Update;
using Delivery.Application.UseCases.Feed.GetAll;
using Delivery.Application.UseCases.Feed.GetById;
using Delivery.Application.UseCases.Feed.Like;
using Delivery.Application.UseCases.Feed.Register;
using Delivery.Application.UseCases.Product.Register;
using Delivery.Application.UseCases.Product.Search;
using Delivery.Application.UseCases.User.GetAll;
using Delivery.Application.UseCases.User.GetById;
using Delivery.Application.UseCases.User.Login;
using Delivery.Application.UseCases.User.Register;
using Delivery.Application.UseCases.User.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Application;

public static class DependecyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterDeliverymanUseCase, RegisterDeliverymanUseCase>();
        services.AddScoped<IGetAllDeliverymanUseCase, GetAllDeliverymanUseCase>();
        services.AddScoped<IGetByIdDeliverymanUseCase, GetByIdDeliverymanUseCase>();
        services.AddScoped<IDeleteDeliverymanUseCase, DeleteDeliverymanUseCase>();
        services.AddScoped<IUpdateDeliverymanUseCase, UpdateDeliverymanUseCase>();
        
        services.AddScoped<IGenerateDeliveyReportPdfUseCase, GenerateDeliveryReportPdfUseCase>();
        
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IGetAllUserUseCase, GetAllUserUseCase>();
        services.AddScoped<IGetByIdUserUseCase, GetByIdUserUseCase>(); 
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>(); 
        
        services.AddScoped<IRegisterFeedUseCase, RegisterFeedUseCase>(); 
        services.AddScoped<IGetByIdFeedUseCase, GetByIdFeedUseCase>(); 
        services.AddScoped<IGetAllFeedUseCase, GetAllFeedUseCase>(); 
        services.AddScoped<ILikeFeedUseCase, LikeFeedUseCase>();

        services.AddScoped<IRegisterProductUseCase, RegisterProductUseCase>();
        services.AddScoped<ISearchProductUseCase, SearchProductUseCase>();

        services.AddScoped<IGenerateDeliveryReportExcelUseCase, GenerateDeliveryReportExcelUseCase>();
        
        services.AddScoped<IBlobStorageService, BlobStorageService>();

        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
    }
}