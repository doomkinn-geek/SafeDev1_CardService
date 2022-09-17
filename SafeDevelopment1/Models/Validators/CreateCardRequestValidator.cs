using CardService.Models.Requests;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CardService.Models.Validators
{
    public class CreateCardRequestValidator : AbstractValidator<CreateCardRequest>
    {
        readonly Regex regExCardName = new Regex(@"[A-Za-z]");
        public CreateCardRequestValidator()
        {
            
            RuleFor(x => x.CardNo)
                .NotNull()
                .NotEmpty()
                .Length(16);
            RuleFor(x => x.CVV2)
                .NotNull()
                .Length(3);
            RuleFor(x => x.ExpDate)
                .GreaterThan(x => DateTime.Now)
                .WithMessage("Карта с истекшим сроком");
            RuleFor(x => x.Name)
                .NotNull()
                .Matches(regExCardName)
                .WithMessage("Имя клиента на карте должно содержать только латинские символы");
        }
    }
}
