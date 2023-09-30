using ApplicationCore.Configurations;
using ApplicationCore.Entities;
using FluentValidation;

namespace ApplicationCore.Validators;

public class QuizQuestionValidator : AbstractValidator<QuizQuestion>
{
    public QuizQuestionValidator()
    {
        RuleFor(x => x.Question).NotNull().Length(1, Constants.MaxTextLength)
            .WithMessage(e => $"Question text must be between 1 and 100 characters");

        RuleFor(x => x.Answers).NotNull();

        RuleFor(x => x.Answers).Must(e => e.Count > 0 && e.Count <= Constants.MaxAnswersPerQuestion)
            .When(e => e.Answers != null)
            .WithMessage(e => $"Questions must have between 1 and 5 answers");

        RuleFor(x => x.Answers).Must(e => e.Count(a => a.Correct) > 0)
            .When(e => e.Answers != null)
            .WithMessage(e => $"Questions must have at least 1 correct answer");

        RuleFor(x => x.Answers).Must(e => e.Count(a => !a.Correct) > 0)
            .When(e => e.Answers != null)
            .WithMessage(e => $"Questions must have at least 1 incorrect answer");

        RuleForEach(e => e.Answers)
            .SetValidator(new QuizAnswerValidator());
    }
}
