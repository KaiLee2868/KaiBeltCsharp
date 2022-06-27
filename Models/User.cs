#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KaiBeltCsharp.Models;
public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    [MinLength(2)]
    public string Name { get; set; }
    
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    public List<Meet> Planned { get; set; } = new List<Meet>();

    public List<Party> Attending { get; set; } = new List<Party>();

    [Required]
    [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Password must contain at least 1 number, 1 letter, and a special character")]
    [DataType(DataType.Password)]
    
    public string Password { get; set; }
    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string ConfirmPW { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    
}




