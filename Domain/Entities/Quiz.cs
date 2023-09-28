using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;
public class Quiz : DatabaseEntity
{
    public string Title { get; set; }
    public string UserId { get; set; }
}
