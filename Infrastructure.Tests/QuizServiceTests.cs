using ApplicationCore.Entities;
using ApplicationCore.Models.Quiz;
using ApplicationCore.Repositories;
using ApplicationCore.Validators;
using AutoMapper;
using Infrastructure.InfrastructureServices;
using Moq;

namespace Infrastructure.Tests;

public class QuizServiceTests
{
    public async Task CreateQuiz_ShouldFailValidation(
        Mock<IBaseRepository<Quiz>> repository,
        Mock<IMapper> mapper)
    {
        var subject = new QuizService(repository.Object, new QuizValidator(), mapper.Object);

        var quizEntity = new Quiz();

        mapper.Setup(m => m.Map<Quiz>(It.IsAny<QuizModel>())).Returns(quizEntity);

        var result = await subject.CreateAsync(It.IsAny<QuizModel>(), It.IsAny<string>());

        repository.Verify(c => c.AddAsync(It.IsAny<Quiz>()), Times.Once);
        mapper.Verify(c => c.Map<Quiz>(It.IsAny<QuizModel>()), Times.Once);

        //result.Should().Be(workItem.Id);
    }
}