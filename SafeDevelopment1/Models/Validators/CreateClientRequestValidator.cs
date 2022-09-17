using CardService.Models.Requests;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CardService.Models.Validators
{
    public class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
    {
        readonly Regex regExName = new Regex(@"[а-яА-Я]");
        public CreateClientRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .Length(2,50)
                .Matches(regExName)
                .WithMessage("Имя должно быть указано кириллицей");
            RuleFor(x => x.Patronymic)
                .NotNull()
                .NotEmpty()
                .Length(2, 50)
                .Matches(regExName)
                .WithMessage("Отчество должно быть указано кириллицей");
            RuleFor(x => x.Surname)
                .NotNull()
                .NotEmpty()
                .Length(2, 50)
                .Matches(regExName)
                .WithMessage("Фамилия должно быть указано кириллицей");
        }
    }
}
