using ApplicationCore.Entities;

namespace ApplicationCore.Specifications.Quizzes;
public class QuizPublishedByIdSpecification : BaseSpecification<Quiz>
{
    public QuizPublishedByIdSpecification(Guid quizId)
        : base(e => e.Id == quizId && e.Published)
    {
        AddInclude("Questions.Answers");
    }
}
