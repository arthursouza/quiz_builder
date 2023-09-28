using ApplicationCore.Entities;
using FluentValidation;

namespace ApplicationCore.Validators;

public class QuizValidator : AbstractValidator<Quiz>
{
    public QuizValidator()
    {
        RuleFor(x => x.Title).NotNull().Length(0, 100);
    }
}
