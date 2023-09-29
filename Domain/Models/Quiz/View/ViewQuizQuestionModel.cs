namespace ApplicationCore.Models.Quiz;
public class ViewQuizQuestionModel
{
    public Guid? Id { get; set; }

    public string Question { get; set; }

    public IList<ViewQuizAnswerModel> Answers { get; set; }
}
