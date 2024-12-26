using Microsoft.AspNetCore.Identity;

namespace Budget_Man.AuthService.Models;

public class User : IdentityUser
{
    public int? year { get; set; }
    public string? displayName { get; set; }
}