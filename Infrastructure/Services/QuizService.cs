using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models.Quiz;
using ApplicationCore.Models.Quiz.Attempt;
using ApplicationCore.Models.Quiz.View;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using ApplicationCore.Specifications.QuizAttempts;
using ApplicationCore.Specifications.Quizzes;
using AutoMapper;
using FluentValidation;

namespace Infrastructure.Services;

public class QuizService : IQuizService
{
    private readonly IBaseRepository<Quiz> _quizRepository;
    private readonly IBaseRepository<QuizAttempt> _quizAttemptRepository;
    private readonly IValidator<Quiz> _validator;
    private readonly IValidator<QuizAttempt> _quizAttemptValidator;
    private readonly IMapper _mapper;

    public QuizService(
        IBaseRepository<Quiz> quizRepository,
        IBaseRepository<QuizAttempt> quizAttemptRepository,
        IValidator<Quiz> validator,
        IMapper mapper,
        IValidator<QuizAttempt> quizAttemptValidator)
    {
        _quizRepository = quizRepository;
        _validator = validator;
        _mapper = mapper;
        _quizAttemptRepository = quizAttemptRepository;
        _quizAttemptValidator = quizAttemptValidator;
    }

    public async Task<Guid> CreateAsync(QuizModel model, string userId)
    {
        var entity = _mapper.Map<Quiz>(model);
        entity.UserId = userId;

        var validationResult = _validator.Validate(entity);
        if (!validationResult.IsValid)
        {
            throw new QuizValidationException(validationResult.Errors);
        }

        await _quizRepository.AddAsync(entity);
        _quizRepository.Save();

        return entity.Id;
    }

    public void Update(UpdateQuizModel model, string userId)
    {
        var databaseEntity = _quizRepository
            .Queryable(new QuizByIdAndUserSpecification(model.Id, userId))
            .FirstOrDefault();

        if (databaseEntity == null)
            throw new KeyNotFoundException();

        var updatedEntity = _mapper.Map<Quiz>(model);

        databaseEntity.Update(updatedEntity);

        var validationResult = _validator.Validate(databaseEntity);
        if (!validationResult.IsValid)
        {
            throw new QuizValidationException(validationResult.Errors);
        }

        _quizRepository.Update(databaseEntity);
        _quizRepository.Save();
    }

    public async Task AnswerAsync(QuizAttemptModel model, string userId)
    {
        var attempt = _mapper.Map<QuizAttempt>(model);
        attempt.UserId = userId;

        var validationResult = _quizAttemptValidator.Validate(attempt);

        if (!validationResult.IsValid)
        {
            throw new QuizValidationException(validationResult.Errors);
        }

        CalculateScore(attempt);

        await _quizAttemptRepository.AddAsync(attempt);
        _quizAttemptRepository.Save();
    }

    private void CalculateScore(QuizAttempt attempt)
    {
        if (!attempt.Questions.Any()) return;

        var quiz = _quizRepository.Queryable(new QuizByIdSpecification(attempt.QuizId)).First();

        foreach (var question in quiz.Questions)
        {
            var questionAttempt = attempt.Questions.FirstOrDefault(q => q.QuestionId == question.Id);
            if (questionAttempt == null) continue;

            var answers = question.Answers.Where(a => a.Correct).Select(a => a.Id).ToList();

            var selectedAnswers = questionAttempt.Answers.Select(a => a.QuizAnswerId).ToList();

            var correctChoices = selectedAnswers.Count(a => answers.Contains(a));
            var incorrectChoices = selectedAnswers.Count(a => !answers.Contains(a));

            var correctWeight = (float)1f / answers.Count();
            var wrongWeight = (float)1f / (question.Answers.Count() - answers.Count());

            questionAttempt.Score = (correctChoices * correctWeight + incorrectChoices * wrongWeight * -1) * 100f;
        }

        attempt.Score = Math.Max(0, attempt.Questions.Sum(q => q.Score) / quiz.Questions.Count);
    }

    public void Remove(Guid id, string userId)
    {
        var quiz = _quizRepository
            .Queryable(new QuizByIdAndUserSpecification(id, userId))
            .FirstOrDefault();

        if (quiz == null)
        {
            throw new KeyNotFoundException();
        }

        _quizRepository.Remove(quiz);
        _quizRepository.Save();

        //var attempts = _quizAttemptRepository.Queryable(new AttemptsByQuizSpecification(id)).ToList();
        //if (!attempts.Any()) return;

        //_quizAttemptRepository.Remove(attempts.ToArray());
        //_quizAttemptRepository.Save();
    }

    public ViewQuizModel Get(Guid id)
    {
        return _mapper.Map<ViewQuizModel>(_quizRepository
            .Queryable(new QuizPublishedByIdSpecification(id))
            .FirstOrDefault());
    }

    public IList<UpdateQuizModel> GetAll(string userId)
    {
        return _mapper.Map<IList<UpdateQuizModel>>(_quizRepository
            .Queryable(new QuizByUserSpecification(userId))
            .ToList());
    }

    public void Publish(Guid id, string userId)
    {
        var databaseEntity = _quizRepository
            .Queryable(new QuizByIdAndUserSpecification(id, userId))
            .FirstOrDefault();

        if (databaseEntity == null) throw new KeyNotFoundException();

        var validationResult = _validator.Validate(databaseEntity);
        if (!validationResult.IsValid)
        {
            throw new QuizValidationException(validationResult.Errors);
        }

        databaseEntity.Published = true;

        _quizRepository.Update(databaseEntity);
        _quizRepository.Save();
    }

    public IList<ViewAttemptModel> GetAllAttempts(string userId)
    {
        return _mapper.Map<IList<ViewAttemptModel>>(_quizAttemptRepository
            .Queryable(new AttemptsByUserSpecification(userId))
            .ToList());
    }

    public IList<ViewAttemptModel> GetAllAttempts(Guid id, string userId)
    {
        return _mapper.Map<IList<ViewAttemptModel>>(_quizAttemptRepository
            .Queryable(new AttemptsForQuizOwnedByUserSpecification(id, userId))
            .ToList());
    }
}
