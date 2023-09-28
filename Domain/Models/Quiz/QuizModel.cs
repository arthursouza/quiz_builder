namespace ApplicationCore.Models.Quiz;
public class QuizModel
{
    public Guid? Id { get; set; }

    public string Title { get; set; }

    public IList<QuizQuestionModel> Questions { get; set; }
}
