using Microsoft.AspNetCore.Identity;


namespace Budget_Man.AuthService.Models;

public class MyUser : IdentityUser
{
    public int? year { get; set; }
    public string? DisplayName { get; set; }

    public string? currency {get; set;}
    
}