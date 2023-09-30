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
            CreateMap<QuizQuestionAnswerModel, QuizAnswer>();
            CreateMap<QuizQuestionModel, QuizQuestion>();
            CreateMap<QuizModel, Quiz>();

            CreateMap<Quiz, ViewQuizModel>();
            CreateMap<QuizQuestion, ViewQuizQuestionModel>();
            CreateMap<QuizAnswer, ViewQuizAnswerModel>();
            CreateMap<QuizAttemptModel, QuizAttempt>()
                .ForMember(e => e.QuizId, f => f.MapFrom(s => s.QuizId))
                .ForMember(e => e.Answers, f => f.MapFrom(s => s.SelectedAnswers
                .Select(a => new QuizAttemptAnswer()
                {
                    QuizAnswerId = a
                })));
        }
    }
}
