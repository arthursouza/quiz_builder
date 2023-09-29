using ApplicationCore.Entities.Common;

namespace ApplicationCore.Entities;
public class QuizQuestion : DatabaseEntity
{
    public string Text { get; set; }

    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public IList<QuizAnswer> Answers { get; set; }

    internal void Update(QuizQuestion quizQuestion)
    {
        Text = quizQuestion.Text;
        UpdateAnswers(quizQuestion.Answers);
    }

    private void UpdateAnswers(IList<QuizAnswer> updatedAnswers)
    {
        var removedAnswers = Answers.Where(q => updatedAnswers.All(e => e.Id != q.Id));
        var addedAnswers = updatedAnswers.Where(q => q.Id == Guid.Empty);

        foreach (var answer in removedAnswers)
        {
            Answers.Remove(answer);
        }

        foreach (var answer in Answers)
        {
            answer.Update(Answers.FirstOrDefault(e => e.Id == answer.Id));
        }

        foreach (var answer in addedAnswers)
        {
            Answers.Add(answer);
        }
    }
}
