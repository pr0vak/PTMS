using Api.ModelsDto;
using FluentValidation;

namespace Api.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(loginDto => loginDto.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(loginDto => loginDto.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}