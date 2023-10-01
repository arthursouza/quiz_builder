using ApplicationCore.Entities.Common;

namespace ApplicationCore.Entities;
public class QuizQuestionAttempt : DatabaseEntity
{
    public float Score { get; set; }

    public Guid QuestionId { get; set; }
    public QuizQuestion Question { get; set; }

    public IList<QuizAnswerAttempt> Answers { get; set; }
}
