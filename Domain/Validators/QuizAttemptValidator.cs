using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Specifications.QuizAttempts;
using ApplicationCore.Specifications.Quizzes;
using FluentValidation;

namespace ApplicationCore.Validators;

public class QuizAttemptValidator : AbstractValidator<QuizAttempt>
{
    private readonly IBaseRepository<Quiz> _quizRepository;
    private readonly IBaseRepository<QuizAttempt> _quizAttemptRepository;

    public QuizAttemptValidator(
        IBaseRepository<QuizAttempt> quizAttemptRepository,
        IBaseRepository<Quiz> quizRepository)
    {
        _quizAttemptRepository = quizAttemptRepository;
        _quizRepository = quizRepository;

        RuleFor(e => GetQuizById(e.QuizId))
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .Must(e => e.Published)
            .WithMessage("The quiz does not exist or is not published");

        RuleFor(e => GetExistingAttempt(e.QuizId, e.UserId)).Null();



        //var answersPerQuestion = new Dictionary<Guid, int>(
        //    quiz.Questions.Select(e => new KeyValuePair<Guid, int>(e.Id, e.Answers.Count(e => e.Correct))));

        //RuleFor(e => e.Answers)
        //    .Must(e =>
        //        e.Select(a => new
        //        {
        //            QuestionId = GetQuizById(a.QuizId).Questions.First(q => q.Answers.Any(qa => qa.Id == a.QuizAnswerId)).Id,
        //            AnswerId = a.QuizAnswerId
        //        })
        //        .GroupBy(a => a.QuestionId)
        //        .All(a => answersPerQuestion[a.Key] > 1 || a.Count() == 1));

    }

    private Quiz GetQuizById(Guid id)
    {
        return _quizRepository.Queryable(new QuizByIdSpecification(id)).FirstOrDefault();
    }

    private QuizAttempt GetExistingAttempt(Guid quizId, string userId)
    {
        return _quizAttemptRepository
            .Queryable(new QuizAttemptByIdAndUserSpecification(quizId, userId))
            .FirstOrDefault();
    }

    //public void Validate(QuizAttempt attempt, Quiz quiz)
    //{
    //    var answersPerQuestion = new Dictionary<Guid, int>(
    //        quiz.Questions.Select(e => new KeyValuePair<Guid, int>(e.Id, e.Answers.Count(e => e.Correct))));

    //    RuleFor(e => e.Answers)
    //        .Must(e =>
    //            e.Select(a => new
    //            {
    //                QuestionId = quiz.Questions.First(q => q.Answers.Any(qa => qa.Id == a.QuizAnswerId)).Id,
    //                AnswerId = a.QuizAnswerId
    //            })
    //            .GroupBy(a => a.QuestionId)
    //            .All(a => answersPerQuestion[a.Key] > 1 || a.Count() == 1));

    //    base.Validate(attempt);
    //}
}