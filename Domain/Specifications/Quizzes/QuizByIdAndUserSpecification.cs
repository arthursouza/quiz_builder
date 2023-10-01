using ApplicationCore.Entities;

namespace ApplicationCore.Specifications.Quizzes;

public class QuizByIdAndUserSpecification : BaseSpecification<Quiz>
{
    public QuizByIdAndUserSpecification(Guid quizId, string userId)
        : base(e => e.Id == quizId && e.UserId == userId)
    {
        AddInclude("Questions.Answers");
    }
}
