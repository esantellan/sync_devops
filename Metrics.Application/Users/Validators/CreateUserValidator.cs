using Metrics.Application.Users.Commands;
using FluentValidation;

namespace Metrics.Application.Users.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("El email es inválido.");
        }
    }
}
