using Api.ModelsDto;
using FluentValidation;

namespace Api.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(registerDto => registerDto.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(registerDto => registerDto.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(5).WithMessage("Password must contain at least 5 characters");

        RuleFor(registerDto => registerDto.FullName)
            .NotEmpty().WithMessage("FullName is required.")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.")
            .Matches(@"^(([А-ЯЁA-Z][а-яёa-z]+ [А-ЯЁA-Z][а-яёa-z]+)|([А-ЯЁA-Z][а-яёa-z]+ [А-ЯЁA-Z][а-яёa-z]+ [А-ЯЁA-Z][а-яёa-z]+))$")
            .WithMessage("The full name must be in the format 'First Name Last Name' or 'Last Name First Name Patronymic', using Russian or English letters.");
    }
}