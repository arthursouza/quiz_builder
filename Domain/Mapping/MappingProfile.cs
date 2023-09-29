using ApplicationCore.Entities;
using ApplicationCore.Models.Quiz;
using ApplicationCore.Models.Quiz.View;
using AutoMapper;

namespace ApplicationCore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<QuizModel, QuizQuestion>().ReverseMap();
            CreateMap<Quiz, ViewQuizModel>();
            CreateMap<QuizQuestion, ViewQuizQuestionModel>();
            CreateMap<QuizAnswer, ViewQuizAnswerModel>();
        }
    }
}
