namespace ApplicationCore.Models.Quiz.Attempt;
public class QuizAttemptModel
{
    public Guid QuizId { get; set; }

    public IList<Guid> SelectedAnswers { get; set; }
}
