using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPIMVC.Models;

[Table("Movie")]
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public double Rating { get; set; }
    public int Rank { get; set; }
}