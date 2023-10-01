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

            CreateMap<QuizAttemptModel, QuizAttempt>()
                .ForMember(e => e.QuizId, f => f.MapFrom(s => s.QuizId))
                .ForMember(e => e.Answers, f => f.MapFrom(s => s.Answers
                .Select(a => new QuizAttemptAnswer()
                {
                    QuizAnswerId = a.AnswerId,
                })));
        }
    }
}
