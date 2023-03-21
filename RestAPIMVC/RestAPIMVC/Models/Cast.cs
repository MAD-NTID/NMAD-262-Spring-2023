using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPIMVC.Models;
[Table("cast")]
public class Cast
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int ActorId { get; set; }
}