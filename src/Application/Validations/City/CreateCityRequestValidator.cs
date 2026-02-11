using Application.DTOs.City;
using FluentValidation;

namespace Application.Validations.City;

public class CreateCityRequestValidator:AbstractValidator<CreateCityRequest>
{
    public CreateCityRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("City name is required.")
            .MinimumLength(2).WithMessage("City name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("City name cannot exceed 100 characters.");

        RuleFor(x => x.Area)
            .GreaterThan(0).WithMessage("Area must be greater than 0.");

        RuleFor(x => x.Population)
            .GreaterThanOrEqualTo(0).WithMessage("Population cannot be negative.");
    }
}
