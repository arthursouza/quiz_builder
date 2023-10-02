using ApplicationCore.Entities.Common;

namespace ApplicationCore.Entities;
public class QuizAnswer : DatabaseEntity
{
    public string Answer { get; set; }

    public bool Correct { get; set; }

    public Guid QuizQuestionId { get; set; }
    public QuizQuestion QuizQuestion { get; set; }

    internal void Update(QuizAnswer quizAnswer)
    {
        Answer = quizAnswer.Answer;
        Correct = quizAnswer.Correct;
    }
}
