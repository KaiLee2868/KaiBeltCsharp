#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KaiBeltCsharp.Models;


public class Party
{
    [Key]
    public int GuestListId { get; set; }
    public int MeetId { get; set; }
    public int UserId { get; set; }
    public User? Guest { get; set; }
    public Meet? Meet { get; set; }  


    
}