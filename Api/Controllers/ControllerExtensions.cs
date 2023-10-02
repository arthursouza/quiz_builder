using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    public static class ControllerExtensions
    {
        public static string GetUserId(this ControllerBase controller)
        {
            return controller.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}