namespace ApplicationCore.Models.Quiz;
public class QuizQuestionModel
{
    public Guid? Id { get; set; }

    public string Question { get; set; }

    public IList<QuizQuestionAnswerModel> Answers { get; set; }
}
