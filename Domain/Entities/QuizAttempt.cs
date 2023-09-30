using ApplicationCore.Entities.Common;

namespace ApplicationCore.Entities;
public class QuizAttempt : DatabaseEntity
{
    public string UserId { get; set; }

    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public float Score { get; set; }

    public List<QuizAttemptAnswer> Answers { get; set; }
}
