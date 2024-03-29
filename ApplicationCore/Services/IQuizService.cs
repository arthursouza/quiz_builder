﻿using ApplicationCore.Models.Quiz;
using ApplicationCore.Models.Quiz.Attempt;
using ApplicationCore.Models.Quiz.View;

namespace ApplicationCore.Services;
public interface IQuizService
{
    Task<Guid> CreateAsync(QuizModel model, string userId);

    void Update(UpdateQuizModel model, string userId);

    Task AnswerAsync(QuizAttemptModel model, string userId);

    void Remove(Guid id, string userId);

    ViewQuizModel Get(Guid id);

    IList<UpdateQuizModel> GetAll(string userId);

    void Publish(Guid id, string userId);

    IList<ViewAttemptModel> GetAllAttempts(string userId, int? page = null, int? size = null);

    IList<ViewAttemptModel> GetAllAttempts(Guid id, string userId, int? page = null, int? size = null);
}
