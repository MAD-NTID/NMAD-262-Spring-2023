using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPIMVC.Models;

[Table("Movie")]
public class Movie
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    [Required]
    public double Rating { get; set; }
    [Required]
    public int Rank { get; set; }
}