namespace ApplicationCore.Models.Quiz;
public class QuizModel
{
    public string Title { get; set; }

    public IList<QuizQuestionModel> Questions { get; set; }
}
