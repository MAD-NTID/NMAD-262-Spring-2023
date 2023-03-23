using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPIMVC.Models;

[Table("user")]
public class User
{
    [Key]
    public string Username { get; set; }
    public string Password { get; set; }
}