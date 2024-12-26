using System.ComponentModel.DataAnnotations;
namespace Budget_Man.AuthService.Models;

public class ForgotPasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}