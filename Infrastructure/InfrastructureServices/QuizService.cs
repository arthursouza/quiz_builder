using ApplicationCore.Entities;
using ApplicationCore.Models.Quiz;
using ApplicationCore.Repositories;
using AutoMapper;
using FluentValidation;

namespace Domain.Services;
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
        //if (!validationResult.IsValid)
        //{
        //    throw new WorkItemValidationException(validationResult.Errors);
        //}

        entity.UserId = userId;
        await _repository.AddAsync(entity);

        _repository.Save();

        return entity.Id;
    }

    public async Task UpdateAsync(QuizModel model, string userId)
    {
        //var dbEntity = _repository.Queryable().FirstOrDefault(e => e.Id == model.Id && e.UserId == userId);
        //if (dbEntity == null)
        //{
        //    throw new KeyNotFoundException();
        //}

        //var entity = _mapper.Map<WorkItem>(model);
        //var validationResult = _validator.Validate(entity);
        //if (!validationResult.IsValid)
        //{
        //    throw new WorkItemValidationException(validationResult.Errors);
        //}

        //dbEntity.Update(entity);
        //await _repository.AddOrUpdateAsync(dbEntity);
        //_repository.Save();
    }

    public void Remove(Guid id, string userId)
    {
        //var dbEntity = _repository.Queryable().FirstOrDefault(e => e.Id == id && e.UserId == userId);
        //if (dbEntity == null)
        //{
        //    throw new KeyNotFoundException();
        //}

        //_repository.Remove(dbEntity);
        //_repository.Save();
    }

    //public WorkItemModel Get(int id, string userId)
    //{
    //    return _mapper.Map<WorkItemModel>(_repository
    //        .Queryable(false)
    //        .FirstOrDefault(e => e.Id == id && e.UserId == userId));
    //}

    //public WorkItemModel[] GetAll(string userId)
    //{
    //    return _mapper.Map<WorkItemModel[]>(_repository
    //        .Queryable(false)
    //        .Where(e => e.UserId == userId)
    //        .OrderBy(e => e.Id)
    //        .ToArray());
    //}
}
