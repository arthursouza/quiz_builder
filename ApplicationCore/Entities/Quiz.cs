using ApplicationCore.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;
public class Quiz : DatabaseEntity
{
    public string Title { get; set; }

    [Required]
    public string UserId { get; set; }

    public bool Published { get; set; }

    public IList<QuizQuestion> Questions { get; set; }

    public void Update(Quiz updatedEntity)
    {
        Title = updatedEntity.Title;
        UpdateQuestions(Questions);
    }

    private void UpdateQuestions(IList<QuizQuestion> updatedQuestions)
    {
        var removedQuestions = Questions.Where(q => updatedQuestions.All(e => e.Id != q.Id));
        var addedQuestions = updatedQuestions.Where(q => q.Id == Guid.Empty);

        foreach (var question in removedQuestions)
        {
            Questions.Remove(question);
        }

        foreach (var question in Questions)
        {
            question.Update(Questions.FirstOrDefault(e => e.Id == question.Id));
        }

        foreach (var question in addedQuestions)
        {
            Questions.Add(question);
        }
    }
}
