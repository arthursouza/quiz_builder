using ApplicationCore.Entities.Common;

namespace ApplicationCore.Entities;
public class QuizAttemptAnswer : DatabaseEntity
{
    public Guid QuizAnswerId { get; set; }
    public QuizAnswer QuizAnswer { get; set; }

    public Guid QuizAttemptId { get; set; }
    public QuizAttempt QuizAttempt { get; set; }
}
