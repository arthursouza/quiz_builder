using ApplicationCore.Configurations;
using ApplicationCore.Entities;
using FluentValidation;

namespace ApplicationCore.Validators;

public class QuizAnswerValidator : AbstractValidator<QuizAnswer>
{
    public QuizAnswerValidator()
    {
        RuleFor(x => x.Answer).NotNull().Length(1, Constants.MaxTextLength);
    }
}
