namespace ApplicationCore.Models.Quiz.Attempt;
public class QuizAttemptModel
{
    public Guid QuizId { get; set; }

    public IList<QuizAnswerAttemptModel> Answers { get; set; }
}
