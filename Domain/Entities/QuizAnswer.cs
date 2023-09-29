using ApplicationCore.Entities.Common;

namespace ApplicationCore.Entities;
public class QuizAnswer : DatabaseEntity
{
    public string Text { get; set; }

    public Guid QuizQuestionId { get; set; }
    public QuizQuestion QuizQuestion { get; set; }

    public bool Correct { get; set; }

    internal void Update(QuizAnswer quizAnswer)
    {
        Text = quizAnswer.Text;
        Correct = quizAnswer.Correct;
    }
}
