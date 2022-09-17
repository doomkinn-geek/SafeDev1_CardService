using CardService.Models.Requests;
using FluentValidation;

namespace CardService.Models.Validators
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotNull()
                .Length(5, 255)
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotNull()
                .Length(5, 50);

        }
    }
}
