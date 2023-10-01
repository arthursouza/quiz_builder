namespace ApplicationCore.Models.Quiz.Attempt;
public class ViewQuestionAttemptModel
{
    public string Question { get; set; }
    public float Score { get; set; }

    public List<string> Answers { get; set; }
}
