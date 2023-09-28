using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class Controller : ControllerBase
{
    protected virtual string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
