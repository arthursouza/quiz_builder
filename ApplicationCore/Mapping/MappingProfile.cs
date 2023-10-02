using ApplicationCore.Entities;
using ApplicationCore.Models.Quiz;
using ApplicationCore.Models.Quiz.Attempt;
using ApplicationCore.Models.Quiz.View;
using AutoMapper;

namespace ApplicationCore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<QuizQuestionAnswerModel, QuizAnswer>().ReverseMap();
            CreateMap<QuizQuestionModel, QuizQuestion>().ReverseMap();
            CreateMap<QuizModel, Quiz>();

            CreateMap<Quiz, UpdateQuizModel>().ReverseMap();

            CreateMap<Quiz, ViewQuizModel>();
            CreateMap<QuizQuestion, ViewQuizQuestionModel>();
            CreateMap<QuizAnswer, ViewQuizAnswerModel>();

            CreateMap<QuizAttempt, ViewAttemptModel>()
                .ForMember(e => e.Title, f => f.MapFrom(s => s.Quiz.Title))
                .ForMember(e => e.Score, e => e.MapFrom(e => e.Score))
                .ForMember(e => e.Questions, e => e.MapFrom(e => e.Questions
                    .Select(q => new ViewQuestionAttemptModel()
                    {
                        Question = q.Question.Question,
                        Score = q.Score,
                        Answers = q.Answers.Select(a => a.QuizAnswer.Answer).ToList()
                    })
                ));

            CreateMap<QuizAttemptModel, QuizAttempt>()
                .ForMember(e => e.QuizId, f => f.MapFrom(s => s.QuizId))
                .ForMember(e => e.Questions, e => e.MapFrom(e => e.Answers
                    .GroupBy(a => a.QuestionId)
                    .Select(group => new QuizQuestionAttempt()
                    {
                        QuestionId = group.Key,
                        Answers = group.Select(a => new QuizAnswerAttempt()
                        {
                            QuestionId = group.Key,
                            QuizAnswerId = a.AnswerId
                        })
                        .ToList()
                    })
                ));
        }
    }
}
