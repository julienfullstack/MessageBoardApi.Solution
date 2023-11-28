using System.ComponentModel;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageBoardApi.Models
{
  public class Posts
  {
    [Key]
    public int PostId { get; set; }
    public int UserId { get; set; }
    public int GroupId { get; set;}

    [ForeignKey("GroupId")]
    public virtual Groups Groups { get; set; }
    public string Name { get; set; }
    public string Message { get; set; }
    public string DateTime { get; set; } 
  }
}