using ApplicationCore.Configurations;
using ApplicationCore.Entities;
using ApplicationCore.Validators;
using FluentAssertions;

namespace ApplicationCore.Tests;

public class ValidatorTests
{
    private readonly QuizValidator _subject;

    public ValidatorTests()
    {
        _subject = new QuizValidator();
    }

    public Quiz CreateValidQuiz()
    {
        return new Quiz()
        {
            Title = "Quiz title",
            Questions = new List<QuizQuestion>()
            {
                CreateValidQuestion(),
                CreateValidQuestion()
            }
        };
    }

    public QuizAnswer CreateValidAnswerIncorrect()
    {
        return new QuizAnswer()
        {
            Answer = "Answer text"
        };
    }

    public QuizAnswer CreateValidAnswerCorrect()
    {
        return new QuizAnswer()
        {
            Answer = "Answer text",
            Correct = true
        };
    }

    public QuizQuestion CreateValidQuestion()
    {
        return new QuizQuestion()
        {
            Question = "Question text",
            Answers = new List<QuizAnswer>()
            {
                CreateValidAnswerIncorrect(),
                CreateValidAnswerCorrect(),
            }
        };
    }

    [Fact]
    public void CreateQuiz_ShouldBeValid()
    {
        var entity = CreateValidQuiz();
        var response = _subject.Validate(entity);
        response.IsValid.Should().Be(true);
    }

    [Fact]
    public void CreateQuiz_ShouldFailTitleValidation()
    {
        var entity = CreateValidQuiz();

        entity.Title = string.Empty;

        var response = _subject.Validate(entity);
        response.IsValid.Should().Be(false);
    }

    [Fact]
    public void CreateQuiz_ShouldFailQuestionCountValidation()
    {
        var entity = CreateValidQuiz();

        for (int i = 0; i <= Constants.MaxQuestionsPerQuiz; i++)
        {
            entity.Questions.Add(CreateValidQuestion());
        }

        var response = _subject.Validate(entity);
        response.IsValid.Should().Be(false);
    }

    [Fact]
    public void CreateQuiz_ShouldFailAnswerCountValidation()
    {
        var entity = CreateValidQuiz();
        var question = entity.Questions.First();

        for (int i = 0; i <= Constants.MaxAnswersPerQuestion; i++)
        {
            question.Answers.Add(CreateValidAnswerCorrect());
        }

        var response = _subject.Validate(entity);
        response.IsValid.Should().Be(false);
    }

    [Fact]
    public void CreateQuiz_ShouldFailCorrectAnswerRequired()
    {
        var entity = CreateValidQuiz();

        foreach (var q in entity.Questions)
        {
            q.Answers.Clear();
            q.Answers.Add(CreateValidAnswerIncorrect());
        }

        var response = _subject.Validate(entity);
        response.IsValid.Should().Be(false);
    }

    [Fact]
    public void CreateQuiz_ShouldFailIncorrectAnswerRequired()
    {
        var entity = CreateValidQuiz();

        foreach (var q in entity.Questions)
        {
            q.Answers.Clear();
            q.Answers.Add(CreateValidAnswerCorrect());
        }

        var subject = new QuizValidator();
        var response = subject.Validate(entity);
        response.IsValid.Should().Be(false);
    }

    [Fact]
    public void CreateQuiz_ShouldFailQuestionsNull()
    {
        var entity = CreateValidQuiz();
        entity.Questions = null;
        var response = _subject.Validate(entity);
        response.IsValid.Should().Be(false);
    }

    [Fact]
    public void CreateQuiz_ShouldFailQuestionsEmpty()
    {
        var entity = CreateValidQuiz();
        entity.Questions.Clear();
        var response = _subject.Validate(entity);
        response.IsValid.Should().Be(false);
    }

    [Fact]
    public void CreateQuiz_ShouldFailAnswersNull()
    {
        var entity = CreateValidQuiz();
        entity.Questions.First().Answers = null;

        var response = _subject.Validate(entity);
        response.IsValid.Should().Be(false);
    }

    [Fact]
    public void CreateQuiz_ShouldFailAnswersEmpty()
    {
        var entity = CreateValidQuiz();
        entity.Questions.First().Answers.Clear();

        var response = _subject.Validate(entity);
        response.IsValid.Should().Be(false);
    }

    [Fact]
    public void CreateQuiz_ShouldFailAnswerTextLengthAboveLimit()
    {
        var entity = CreateValidQuiz();

        entity.Questions.First().Answers.First().Answer = new string('a', Constants.MaxTextLength + 1);

        var response = _subject.Validate(entity);
        response.IsValid.Should().Be(false);
    }

    [Fact]
    public void CreateQuiz_ShouldFailQuestionTextLengthAboveLimit()
    {
        var entity = CreateValidQuiz();

        entity.Questions.First().Question = new string('a', Constants.MaxTextLength + 1);

        var response = _subject.Validate(entity);
        response.IsValid.Should().Be(false);
    }

    [Fact]
    public void CreateQuiz_ShouldFailQuizTitleLengthAboveLimit()
    {
        var entity = CreateValidQuiz();

        entity.Title = new string('a', Constants.MaxTextLength + 1);

        var response = _subject.Validate(entity);
        response.IsValid.Should().Be(false);
    }
}