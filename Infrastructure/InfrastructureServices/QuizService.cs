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

namespace Infrastructure.InfrastructureServices;
public class QuizService : IQuizService
{
    private readonly IBaseRepository<Quiz> _quizRepository;
    private readonly IBaseRepository<QuizAttempt> _quizAttemptRepository;
    private readonly IValidator<Quiz> _validator;
    private readonly IMapper _mapper;

    public QuizService(
        IBaseRepository<Quiz> quizRepository,
        IBaseRepository<QuizAttempt> quizAttemptRepository,
        IValidator<Quiz> validator,
        IMapper mapper)
    {
        _quizRepository = quizRepository;
        _validator = validator;
        _mapper = mapper;
        _quizAttemptRepository = quizAttemptRepository;
    }

    public async Task<Guid> CreateAsync(QuizModel model, string userId)
    {
        var entity = _mapper.Map<Quiz>(model);

        var validationResult = _validator.Validate(entity);
        if (!validationResult.IsValid)
        {
            throw new QuizValidationException(validationResult.Errors);
        }

        entity.UserId = userId;

        await _quizRepository.AddAsync(entity);
        _quizRepository.Save();

        return entity.Id;
    }

    public async Task UpdateAsync(UpdateQuizModel model, string userId)
    {
        var databaseEntity = _quizRepository
            .Queryable(new QuizByIdAndUserSpecification(model.Id, userId))
            .FirstOrDefault();

        if (databaseEntity.Published)
        {
            throw new QuizValidationException("Unable to edit a published quiz");
        }

        var updatedEntity = _mapper.Map<Quiz>(model);

        databaseEntity.Update(updatedEntity);

        var validationResult = _validator.Validate(databaseEntity);
        if (!validationResult.IsValid)
        {
            throw new QuizValidationException(validationResult.Errors);
        }
    }

    public async Task AnswerAsync(QuizAttemptModel model, string userId)
    {
        var databaseEntity = _quizRepository
            .Queryable(new QuizByIdSpecification(model.QuizId))
            .FirstOrDefault();

        if (!databaseEntity.Published)
        {
            throw new QuizValidationException("Unable to answer an unpublished quiz");
        }

        var currentAttempt = _quizAttemptRepository
            .Queryable(new QuizAttemptByIdAndUserSpecification(model.QuizId, userId))
            .FirstOrDefault();

        if (currentAttempt != null)
        {
            throw new QuizValidationException("You have already answered this quiz");
        }

        var attempt = _mapper.Map<QuizAttempt>(model);

        // Validate at least one answer

        await _quizAttemptRepository.AddAsync(attempt);
        _quizAttemptRepository.Save();
    }

    public void Remove(Guid id, string userId)
    {
        var databaseEntity = _quizRepository
            .Queryable(new QuizByIdAndUserSpecification(id, userId))
            .FirstOrDefault();

        //var dbEntity = _repository.Queryable().FirstOrDefault(e => e.Id == id && e.UserId == userId);
        //if (dbEntity == null)
        //{
        //    throw new KeyNotFoundException();
        //}

        //_repository.Remove(dbEntity);
        //_repository.Save();
    }

    public ViewQuizModel Get(Guid id)
    {
        return _mapper.Map<ViewQuizModel>(_quizRepository
            .Queryable(new QuizByIdSpecification(id))
            .FirstOrDefault());
    }

    //public WorkItemModel[] GetAll(string userId)
    //{
    //    return _mapper.Map<WorkItemModel[]>(_repository
    //        .Queryable(false)
    //        .Where(e => e.UserId == userId)
    //        .OrderBy(e => e.Id)
    //        .ToArray());
    //}
}
