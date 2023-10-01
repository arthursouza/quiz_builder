namespace ApplicationCore.Models.Quiz.Attempt;
public class ViewAttemptModel
{
    public string Title { get; set; }
    public float Score { get; set; }

    public IList<ViewQuestionAttemptModel> Questions { get; set; }
}
