using ApplicationCore.Entities;

namespace ApplicationCore.Specifications.QuizAttempts;

public class AttemptsForQuizOwnedByUserSpecification : BaseSpecification<QuizAttempt>
{
    public AttemptsForQuizOwnedByUserSpecification(Guid quizId, string userId)
        : base(e => e.QuizId == quizId && e.Quiz.UserId == userId)
    {
        AddInclude($"{nameof(QuizAttempt.Quiz)}");
        AddInclude($"{nameof(QuizAttempt.Questions)}.{nameof(QuizQuestionAttempt.Answers)}");
        AddInclude($"{nameof(QuizAttempt.Questions)}.{nameof(QuizQuestionAttempt.Question)}");
        AddInclude($"{nameof(QuizAttempt.Questions)}.{nameof(QuizQuestionAttempt.Answers)}.{nameof(QuizAnswerAttempt.QuizAnswer)}");
    }
}
