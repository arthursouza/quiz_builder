namespace ApplicationCore.Models.Quiz;
public class UpdateQuizModel : QuizModel
{
    public Guid Id { get; set; }

    public bool Published { get; set; }
}
