using ApplicationCore.Models.Quiz;

namespace Domain.Services;
public interface IQuizService
{
    Task<Guid> CreateAsync(QuizModel model, string userId);

    Task UpdateAsync(QuizModel model, string userId);

    void Remove(Guid id, string userId);
}
