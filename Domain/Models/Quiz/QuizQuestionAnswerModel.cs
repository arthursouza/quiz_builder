namespace ApplicationCore.Models.Quiz;
public class QuizQuestionAnswerModel
{
    public Guid? Id { get; set; }

    public string Text { get; set; }

    public bool Correct { get; set; }

    public int QuizQuestionModelId { get; set; }
}
