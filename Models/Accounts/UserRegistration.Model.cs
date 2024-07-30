using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

public class UserRegistration {
    [Required(ErrorMessage = "Must enter Email.")]
    [EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage = "How can you register without a password ?")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserRegistration, IdentityUser>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
    }
}