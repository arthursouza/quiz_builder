using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.User;
public class AuthModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
