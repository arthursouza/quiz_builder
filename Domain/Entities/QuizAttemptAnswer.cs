using ApplicationCore.Entities.Common;

namespace ApplicationCore.Entities;
public class QuizAttemptAnswer : DatabaseEntity
{
    public string Text { get; set; }

    public Guid QuizQuestionId { get; set; }
    public QuizQuestion QuizQuestion { get; set; }

    public bool Correct { get; set; }
}
