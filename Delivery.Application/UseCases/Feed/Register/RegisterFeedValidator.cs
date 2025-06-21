using Delivery.Comunication.RequestModel.Feed;
using Delivery.Exception;
using FluentValidation;

namespace Delivery.Application.UseCases.Feed.Register;

public class RegisterFeedValidator : AbstractValidator<RequestRegisterFeedJson>
{
    public RegisterFeedValidator()
    {
        RuleFor(feed => feed.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
        RuleFor(feed => feed.Duration).NotEmpty().LessThanOrEqualTo(5).WithMessage(ResourceErrorMessages.DURATION_MAX_LIMIT);
        RuleFor(feed => feed.ThumbnailUrl).NotEmpty().WithMessage(ResourceErrorMessages.THUMB_REQUIRED);
        RuleFor(feed => feed.VideoUrl).NotEmpty().WithMessage(ResourceErrorMessages.URLVIDEO_REQUIRED);
    }
}