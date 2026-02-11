using Application.DTOs.PropertyAd;
using FluentValidation;

namespace Application.Validations.PropertyAd;

public class CreatePropertyAdRequestValidator : AbstractValidator<CreatePropertyAdRequest>
{
    public CreatePropertyAdRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MinimumLength(3).WithMessage("Location must be at least 3 characters long.");

        RuleFor(x => x.RoomCount)
            .GreaterThan(0).WithMessage("Room count must be at least 1.");

        RuleFor(x => x.Area)
            .GreaterThan(0).WithMessage("Area must be greater than 0.");

        RuleFor(x => x.OfferType)
            .IsInEnum().WithMessage("OfferType is invalid.");

        RuleFor(x => x.RealEstateType)
            .IsInEnum().WithMessage("RealEstateType is invalid.");
    }
}