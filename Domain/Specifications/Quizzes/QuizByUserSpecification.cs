using ApplicationCore.Entities;

namespace ApplicationCore.Specifications.Quizzes;
public class QuizByUserSpecification : BaseSpecification<Quiz>
{
    public QuizByUserSpecification(string userId)
        : base(e => e.UserId == userId)
    {
        AddInclude("Questions.Answers");
    }
}
