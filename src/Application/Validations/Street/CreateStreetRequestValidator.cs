using Application.DTOs.Street;
using FluentValidation;

namespace Application.Validations.Street;

public class CreateStreetRequestValidator: AbstractValidator<CreateStreetRequest>
{
    public CreateStreetRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Street name is required.")
            .MaximumLength(100).WithMessage("Street name must not exceed 100 characters.");
        RuleFor(x => x.Length)
            .GreaterThan(0).WithMessage("Street length must be greater than zero.");
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        RuleFor(x => x.CityId)
            .GreaterThan(0).WithMessage("City ID must be a positive integer.");
    }
}
