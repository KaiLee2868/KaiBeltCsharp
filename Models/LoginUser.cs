#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KaiBeltCsharp.Models;

public class LoginUser
    {
    [EmailAddress]
    [Required]
    public string LogEmail {get; set;}
    [Required]
    [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
    [DataType(DataType.Password)]
    public string LogPassword { get; set; }
    }