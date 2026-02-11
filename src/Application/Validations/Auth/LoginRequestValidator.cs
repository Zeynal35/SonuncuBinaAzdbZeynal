namespace Application.Validations.Auth;

using FluentValidation;
using Application.DTOs.Auth;

public class LoginRequestValidator : AbstractValidator<LoginReq>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login boş ola bilməz");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password boş ola bilməz");
    }
}
