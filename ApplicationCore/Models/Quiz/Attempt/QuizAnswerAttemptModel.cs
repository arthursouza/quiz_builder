namespace ApplicationCore.Models.Quiz.Attempt;
public class QuizAnswerAttemptModel
{
    public Guid QuestionId { get; set; }

    public Guid AnswerId { get; set; }
}
