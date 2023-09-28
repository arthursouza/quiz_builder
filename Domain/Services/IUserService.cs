using ApplicationCore.Models.User;

namespace ApplicationCore.Services;
public interface IUserService
{
    Task CreateAsync(AuthModel model);
    Task<string> Authenticate(AuthModel model);
}
