namespace ApplicationCore.Models.Quiz;
public class QuizQuestionAnswerModel
{
    public Guid? Id { get; set; }

    public string Answer { get; set; }

    public bool Correct { get; set; }
}
