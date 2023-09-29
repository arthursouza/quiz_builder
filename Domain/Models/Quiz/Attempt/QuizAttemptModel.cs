namespace ApplicationCore.Models.Quiz.Attempt;
public class QuizAttemptModel
{
    public Guid Id { get; set; }

    public IList<Guid> SelectedAnswers { get; set; }
}
