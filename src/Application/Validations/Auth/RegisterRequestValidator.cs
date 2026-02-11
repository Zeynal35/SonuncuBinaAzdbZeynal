using FluentValidation;
using System.Reflection;

namespace Application.Validations.Auth;

using FluentValidation;
using Application.DTOs.Auth;

public class RegisterRequestValidator : AbstractValidator<RegisterReq>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName boş ola bilməz")
            .MinimumLength(3).WithMessage("UserName minimum 3 simvol olmalıdır")
            .MaximumLength(50).WithMessage("UserName maksimum 50 simvol ola bilər");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz")
            .EmailAddress().WithMessage("Email düzgün formatda deyil");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password boş ola bilməz")
            .MinimumLength(8).WithMessage("Password minimum 8 simvol olmalıdır")
            .Matches("[A-Z]").WithMessage("Password ən azı 1 böyük hərf içerməlidir")
            .Matches("[a-z]").WithMessage("Password ən azı 1 kiçik hərf içerməlidir")
            .Matches("[0-9]").WithMessage("Password ən azı 1 rəqəm içerməlidir")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password ən azı 1 simvol içerməlidir");
    }
}
