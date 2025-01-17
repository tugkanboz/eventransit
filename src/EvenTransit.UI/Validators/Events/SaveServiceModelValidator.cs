using EvenTransit.Domain.Constants;
using EvenTransit.UI.Models.Events;
using FluentValidation;

namespace EvenTransit.UI.Validators.Events;

public class SaveServiceModelValidator : AbstractValidator<SaveServiceModel>
{
    public SaveServiceModelValidator()
    {
        RuleFor(x => x.EventId)
            .Must(x => x != Guid.Empty)
            .WithMessage("Event id cannot be empty");

        RuleFor(x => x.ServiceName)
            .NotEmpty()
            .WithMessage("Service name cannot be empty");

        RuleFor(x => x.Url)
            .NotEmpty()
            .WithMessage("Url cannot be empty");

        RuleFor(x => x.ServiceName)
            .Matches(@"^([a-zA-Z\-])+$")
            .WithMessage(ValidationConstants.ServiceNameRegexError);

        RuleFor(x => x.Timeout)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidationConstants.TimeoutMustBeGreaterThanZero);

        RuleFor(x => x.DelaySeconds)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidationConstants.DelaySecondsMustBeGreaterThanZero);
        
        RuleFor(x => x.Method)
            .NotEmpty()
            .WithMessage(ValidationConstants.MethodCannotBeNotEmpty);

        RuleFor(x => x.Method)
            .Must(x => x is "GET" or "POST" or "PUT" or "PATCH" or "DELETE" or "HEAD" or "OPTIONS")
            .When(x => !string.IsNullOrWhiteSpace(x.Method))
            .WithMessage(ValidationConstants.InvalidMethod);
    }
}
