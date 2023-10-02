using ApplicationCore.Entities.Common;

namespace ApplicationCore.Entities;
public class QuizAnswerAttempt : DatabaseEntity
{
    public float Score { get; set; }

    public Guid QuizAnswerId { get; set; }
    public QuizAnswer QuizAnswer { get; set; }

    public Guid QuestionId { get; set; }
    public QuizQuestionAttempt Question { get; set; }
}
