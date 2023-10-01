using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models.Quiz;
using ApplicationCore.Models.Quiz.Attempt;
using ApplicationCore.Models.Quiz.View;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
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

    public void Update(UpdateQuizModel model, string userId)
    {
        var databaseEntity = _quizRepository
            .Queryable(new QuizByIdAndUserSpecification(model.Id, userId))
            .FirstOrDefault();

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

        var validationResult = _quizAttemptValidator.Validate(attempt);

        if (!validationResult.IsValid)
        {
            throw new QuizValidationException(validationResult.Errors);
        }

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

        var validationResult = _validator.Validate(databaseEntity);
        if (!validationResult.IsValid)
        {
            throw new QuizValidationException(validationResult.Errors);
        }

        databaseEntity.Published = true;

        _quizRepository.Update(databaseEntity);
        _quizRepository.Save();
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
