using ApplicationCore.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;
public class QuizAttempt : DatabaseEntity
{
    [Required]
    public string UserId { get; set; }

    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public float Score { get; set; }

    public List<QuizQuestionAttempt> Questions { get; set; }
}
