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

namespace Infrastructure.InfrastructureServices;
public class QuizService : IQuizService
{
    private readonly IBaseRepository<Quiz> _repository;
    private readonly IValidator<Quiz> _validator;
    private readonly IMapper _mapper;

    public QuizService(
        IBaseRepository<Quiz> repository,
        IValidator<Quiz> validator,
        IMapper mapper)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
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

        await _repository.AddAsync(entity);
        _repository.Save();

        return entity.Id;
    }

    public async Task UpdateAsync(UpdateQuizModel model, string userId)
    {
        var databaseEntity = _repository
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
        var databaseEntity = _repository
            .Queryable(new QuizByIdSpecification(model.Id))
            .FirstOrDefault();

        if (!databaseEntity.Published)
        {
            throw new QuizValidationException("Unable to answer an unpublished quiz");
        }

        //var updatedEntity = _mapper.Map<Quiz>(model);

        //databaseEntity.Update(updatedEntity);

        //var validationResult = _validator.Validate(databaseEntity);
        //if (!validationResult.IsValid)
        //{
        //    throw new QuizValidationException(validationResult.Errors);
        //}
    }

    public void Remove(Guid id, string userId)
    {
        var databaseEntity = _repository
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
        return _mapper.Map<ViewQuizModel>(_repository
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
