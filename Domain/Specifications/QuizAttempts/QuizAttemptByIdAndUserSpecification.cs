using ApplicationCore.Entities;

namespace ApplicationCore.Specifications.QuizAttempts;

public class QuizAttemptByIdAndUserSpecification : BaseSpecification<QuizAttempt>
{
    public QuizAttemptByIdAndUserSpecification(Guid quizId, string userId)
        : base(e => e.Id == quizId && e.UserId == userId)
    {
    }
}
