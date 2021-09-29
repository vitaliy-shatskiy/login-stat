using FluentValidation;
using LoginStat.Common.Dto.Users;

namespace LoginStat.Common.Validators.Users
{
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(30)
                .WithMessage("Maximum length is 30");


            RuleFor(dto => dto.Surname)
                .NotEmpty()
                .WithMessage("Surname is required")
                .MaximumLength(30)
                .WithMessage("Maximum length is 30");

            RuleFor(dto => dto.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Bad email address format")
                .MaximumLength(25)
                .WithMessage("Maximum length is 25");
        }
    }
}