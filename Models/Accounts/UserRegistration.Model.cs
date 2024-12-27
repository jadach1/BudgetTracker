using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Budget_Man.AuthService.Models;
public class UserRegistration {
    [Required(ErrorMessage = "Must enter Email.")]
    [EmailAddress]
    public string ?Email { get; set; }

     [Required(ErrorMessage = "Must enter Display Name.")]
    public string ?DisplayName { get; set; }

    [Required(ErrorMessage = "How can you register without a password ?")]
    [DataType(DataType.Password)]
    public string ?Password { get; set; }
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ?ConfirmPassword { get; set; }
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserRegistration, MyUser>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
    }
}