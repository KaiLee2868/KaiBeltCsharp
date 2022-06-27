#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KaiBeltCsharp.Models;
public class Meet
{
    [Key]
    public int MeetId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]

    [Future]    
    [DataType(DataType.DateTime)]
    public DateTime DateandTime { get; set; }
    [Required]
    public int Duration {get; set;}
    [Required]
    public string DurationStyle{get; set;}
    public int UserId { get; set; }

    public User? Creator { get; set; }
 
    public List<Party> Guests { get; set; } = new List<Party>();



    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}

public class FutureAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (((DateTime)value) < DateTime.Now)
            return new ValidationResult("Date must be in the Future!");
        return ValidationResult.Success;
    }
}