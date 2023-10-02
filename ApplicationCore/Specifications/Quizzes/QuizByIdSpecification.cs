using ApplicationCore.Entities;

namespace ApplicationCore.Specifications.Quizzes;
public class QuizByIdSpecification : BaseSpecification<Quiz>
{
    public QuizByIdSpecification(Guid quizId)
        : base(e => e.Id == quizId)
    {
        AddInclude($"{nameof(Quiz.Questions)}.{nameof(QuizQuestion.Answers)}");
    }
}
