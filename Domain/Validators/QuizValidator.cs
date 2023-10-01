using ApplicationCore.Configurations;
using ApplicationCore.Entities;
using FluentValidation;

namespace ApplicationCore.Validators;

public class QuizValidator : AbstractValidator<Quiz>
{
    public QuizValidator()
    {
        RuleFor(x => x.Title).NotNull().Length(1, Constants.MaxTextLength);

        RuleFor(x => x.Questions).NotNull();

        RuleFor(x => x.Published)
            .Must(published => published == false)
            .WithMessage("Unable to edit a published quiz");

        RuleFor(e => e.Questions)
            .Must(e => e.Count > 0 && e.Count <= Constants.MaxQuestionsPerQuiz)
            .When(e => e.Questions != null)
            .WithMessage("Must have between 1 and 10 questions");

        RuleForEach(e => e.Questions).SetValidator(new QuizQuestionValidator());
    }
}