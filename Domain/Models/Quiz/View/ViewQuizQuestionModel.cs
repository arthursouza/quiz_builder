namespace ApplicationCore.Models.Quiz.View;
public class ViewQuizQuestionModel
{
    public Guid? Id { get; set; }

    public string Question { get; set; }

    public IList<ViewQuizAnswerModel> Answers { get; set; }
}
