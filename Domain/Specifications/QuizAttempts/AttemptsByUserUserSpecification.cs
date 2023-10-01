using ApplicationCore.Entities;

namespace ApplicationCore.Specifications.QuizAttempts;

public class AttemptsByUserSpecification : BaseSpecification<QuizAttempt>
{
    public AttemptsByUserSpecification(string userId)
        : base(e => e.UserId == userId)
    {
        AddInclude($"{nameof(QuizAttempt.Quiz)}");
        AddInclude($"{nameof(QuizAttempt.Questions)}.{nameof(QuizQuestionAttempt.Answers)}");
        AddInclude($"{nameof(QuizAttempt.Questions)}.{nameof(QuizQuestionAttempt.Question)}");
        AddInclude($"{nameof(QuizAttempt.Questions)}.{nameof(QuizQuestionAttempt.Answers)}.{nameof(QuizAnswerAttempt.QuizAnswer)}");
    }
}
