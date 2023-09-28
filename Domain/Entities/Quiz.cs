using ApplicationCore.Entities.Common;

namespace ApplicationCore.Entities;
public class Quiz : DatabaseEntity
{
    public string Title { get; set; }

    public string UserId { get; set; }
}
