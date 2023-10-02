using FluentValidation.Results;

namespace ApplicationCore.Exceptions;
public class QuizValidationException : Exception
{
    public QuizValidationException(string message) : base(message)
    {
    }

    public QuizValidationException(List<ValidationFailure> errors) : base(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)))
    {
    }
}
